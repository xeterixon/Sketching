using System;
using System.Collections.Generic;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using Sketching.Tool;
using Sketching.Helper;
using SkiaSharp;
using Xamarin.Forms;
namespace Sketching.Views
{
	public class SketchArea : View, ITouchDelegate, ISketchArea
	{
		//TODO Is this a bit overly designed? We only got one delegate and thats the ToolCollection object....
		public List<ITouchDelegate> Delegates { get; set; } = new List<ITouchDelegate>();
		private IToolCollection _tools;
		private GridRenderer _gridRenderer = new GridRenderer();
		private BackgroundImageRenderer _backgroundImageRenderer = new BackgroundImageRenderer();
		public static readonly BindableProperty CanvasBackgroundColorProperty = BindableProperty.Create(nameof(CanvasBackgroundColor), typeof(Color), typeof(SketchArea), Color.FromHex("#F0F8FF"));
		public Color CanvasBackgroundColor
		{
			get { return (Color)GetValue(CanvasBackgroundColorProperty); }
			set { SetValue(CanvasBackgroundColorProperty, value); }
		}
		public static readonly BindableProperty CanDrawOutsideImageBoundsProperty = BindableProperty.Create(nameof(CanDrawOutsideImageBounds), typeof(bool), typeof(SketchArea), true);
		public bool CanDrawOutsideImageBounds
		{
			get { return (bool)GetValue(CanDrawOutsideImageBoundsProperty); }
			set { SetValue(CanDrawOutsideImageBoundsProperty, value); }
		}
		public bool EnableGrid 
		{
			get { return _gridRenderer.Enabled; }
			set { _gridRenderer.Enabled = value;}
		}
		private bool RestictArea => CanDrawOutsideImageBounds == false && BackgroundImage != null;
		private Size LastCanvasSize = new Size();
		private SKImage _snapShot;
		public SKImage SnapShot
		{
			get { return _snapShot; }
			private set
			{
				_snapShot?.Dispose();
				_snapShot = value;
			}
		}
		public IImage BackgroundImage
		{
			get { return _backgroundImageRenderer.Image; }
			set
			{
				_backgroundImageRenderer.Image = value;
				Redraw();
			}
		}
		public IToolCollection ToolCollection
		{
			get { return _tools; }
			set
			{
				_tools = value;
				_tools.View = this;
			}
		}
		public void Redraw()
		{
			CallbackToNative?.Invoke(CallbackType.Repaint);
		}
		public IImage LargeImageData()
		{
			var image = RenderToOriginalResolutionAndClipToImage();
			return new BackgroundImage
			{
				Data = image.Encode(SKEncodedImageFormat.Jpeg,  100).ToArray(),
				Width = image.Width,
				Height = image.Height
			};
		}
		public IImage ImageData()
		{
			var image = new BackgroundImage();
			if (_snapShot != null)
			{
				image.Data = _snapShot.Encode(SKEncodedImageFormat.Jpeg, 100).ToArray();
				image.Width = _snapShot.Width;
				image.Height = _snapShot.Height;
			}
			return image;
		}
		public SKImage RenderToOriginalResolutionAndClipToImage()
		{
			var scale = 1.0;
			// get the background image, if any, to scale things
			var bgWidth = BackgroundImage?.Width;
			var w = LastCanvasSize.Width;
			var h = LastCanvasSize.Height;
			if (bgWidth != null)
			{
				scale = bgWidth.Value / LastCanvasSize.Width;
				//Clip to image. 
				if (!CanDrawOutsideImageBounds)
				{
					scale = bgWidth.Value / _backgroundImageRenderer.ImageDisplayWidth;
					w = BackgroundImage.Width / scale;
					h = BackgroundImage.Height / scale;
				}

			}
			using (var surface = SKSurface.Create((int)(w * scale), (int)(h * scale), SKImageInfo.PlatformColorType, SKAlphaType.Premul))
			{
				Draw(surface, scale);
				return surface.Snapshot();
			}
		}
		public Action<CallbackType> CallbackToNative { get; set; }
		private void Draw(SKSurface surface, double scale)
		{
			var canvas = surface.Canvas;
			surface.Canvas.Clear(CanvasBackgroundColor.ToSkiaColor());
			_gridRenderer.Render(canvas, scale);
			if (ToolCollection == null) return;
			_backgroundImageRenderer.Render(canvas, scale);
			foreach (var geom in ToolCollection.Geometries)
			{
				GeometryRenderer.Render(canvas, geom, scale);
			}
			//TODO Try to do this on demand, rather than every draw...
			// Snapshotting without encoding is rather fast though and does not impact performance that much
			SnapShot = surface.Snapshot();

			//TODO Look into this
			// Not really needed, but keeps the memory slightly lower and does not impact performance that much
			var mem = GC.GetTotalMemory(true);
			System.Diagnostics.Debug.WriteLine($"Mem {mem}");
		}
		public virtual void Draw(SKSurface surface, SKImageInfo info)
		{
			LastCanvasSize.Width = surface.Canvas.ClipDeviceBounds.Width;
			LastCanvasSize.Height = surface.Canvas.ClipBounds.Height;
			Draw(surface, 1.0);
		}
		public virtual void TouchStart(Point p)
		{
			if (!IsTouchPointValid(p)) return;
			foreach (var item in Delegates)
			{
				item.TouchStart(p);
			}
		}
		public virtual void TouchEnd(Point p)
		{
			foreach (var item in Delegates)
			{
				item.TouchEnd(p);
			}
		}
		public virtual void TouchMove(Point p)
		{
			if (!IsTouchPointValid(p))
			{
				TouchEnd(p);
				return;
			}
			foreach (var item in Delegates)
			{
				item.TouchMove(p);
			}
		}
		private bool IsTouchPointValid(Point p)
		{
			if (!RestictArea) return true;
			if (p.X > _backgroundImageRenderer.ImageDisplayWidth) return false;
			if (p.Y > _backgroundImageRenderer.ImageDisplayHeight) return false;
			return true;
		}
	}
}

