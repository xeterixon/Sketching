﻿using System;
using System.Collections.Generic;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using Sketching.Common.Render;
using Sketching.Common.Renderer;
using Sketching.Common.Tools;
using SkiaSharp;
using Xamarin.Forms;
namespace Sketching.Common.Views
{
	public class SketchArea : View, ITouchDelegate, ISketchView
	{
		//TODO Is this a bit overly designed? We only got one delegate and thats the ToolCollection object....
		public List<ITouchDelegate> Delegates { get; set; } = new List<ITouchDelegate>();
		private IToolCollection _tools;
		private GridRenderer _gridRenderer = new GridRenderer ();
		private BackgroundImageRenderer _backgroundImageRenderer = new BackgroundImageRenderer();
		public static readonly BindableProperty CanvasBackgroundColorProperty = BindableProperty.Create(nameof(CanvasBackgroundColor), typeof(Color), typeof(SketchArea), Color.FromHex("#F0F8FF"));
		public Color CanvasBackgroundColor {
			get { return (Color)GetValue(CanvasBackgroundColorProperty); }
			set { SetValue(CanvasBackgroundColorProperty, value); }
		}
		private Size LastCanvasSize = new Size();
		private SKImage _snapShot;
		public SKImage SnapShot {
			get { return _snapShot; }
			private set 
			{
				_snapShot?.Dispose();
				_snapShot = value;
			}
		}
		public IImage BackgroundImage {
			get { return _backgroundImageRenderer.Image;}
			set 
			{
				_backgroundImageRenderer.Image = value;
				Redraw();
			}
		}
		public IToolCollection ToolCollection {
			get { return _tools; }
			set {
				_tools = value;
				_tools.View = this;
			}
		}
		public void Redraw() 
		{
			CallbackToNative?.Invoke(CallbackType.Repaint);
		}
		public byte[] LargeImageData() 
		{
			var image = RenderToFullResolution();
			return image.Encode(SKImageEncodeFormat.Jpeg, 100).ToArray();
		}
		public byte[] ImageData()
		{
			return _snapShot?.Encode(SKImageEncodeFormat.Jpeg,100)?.ToArray();
		}
		public SKImage RenderToFullResolution() 
		{
			var scale = 1.0;
			// get the background image, if any, to scale things
			var bgWidth = BackgroundImage?.Width;
			if (bgWidth != null) 
			{
				scale = bgWidth.Value / LastCanvasSize.Width;
			}
			using (var surface = SKSurface.Create((int)(LastCanvasSize.Width * scale), (int)(LastCanvasSize.Height * scale), SKImageInfo.PlatformColorType, SKAlphaType.Premul)) 
			{
				Draw(surface, scale);
				return surface.Snapshot();
			}
		}
		public Action<CallbackType> CallbackToNative { get; set; }
		private void Draw(SKSurface surface , double scale) 
		{
			var canvas = surface.Canvas;
			surface.Canvas.Clear(CanvasBackgroundColor.ToSkiaColor());
			_gridRenderer.Render(canvas, scale);
			if (ToolCollection == null) return;
			_backgroundImageRenderer.Render(canvas, scale);
			foreach (var geom in ToolCollection.Geometries) {
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
			foreach (var item in Delegates) {
				item.TouchStart(p);
			}
		}
		public virtual void TouchEnd(Point p)
		{
			foreach (var item in Delegates) {
				item.TouchEnd(p);
			}
		}
		public virtual void TouchMove(Point p)
		{

			foreach (var item in Delegates) {
				item.TouchMove(p);
			}
		}
	}
}

