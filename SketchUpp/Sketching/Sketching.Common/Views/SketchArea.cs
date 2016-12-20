using System;
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
		private SKImage _snapShot;
		private BackgroundImageRenderer _backgroundImageRenderer = new BackgroundImageRenderer();
		public static readonly BindableProperty CanvasBackgroundColorProperty = BindableProperty.Create(nameof(CanvasBackgroundColor), typeof(Color), typeof(SketchArea), Color.FromHex("#F0F8FF"));
		public Color CanvasBackgroundColor {
			get { return (Color)GetValue(CanvasBackgroundColorProperty); }
			set { SetValue(CanvasBackgroundColorProperty, value); }
		}

		public IImage BackgroundImage {
			get { return _backgroundImageRenderer.Image;}
			set 
			{
				_backgroundImageRenderer.Image = value;
				Redraw();
			}
		}
		// The background image as a bitmap.
		// A tad faster to render.
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
		public byte[] ImageData()
		{
			return _snapShot?.Encode(SKImageEncodeFormat.Jpeg,100)?.ToArray();
		}
		public Action<CallbackType> CallbackToNative { get; set; }

		public virtual void Draw(SKSurface surface, SKImageInfo info)
		{
			var canvas = surface.Canvas;
			surface.Canvas.Clear(CanvasBackgroundColor.ToSkiaColor());
			if (ToolCollection == null) return;
			_backgroundImageRenderer.Render(canvas);
			_gridRenderer.Render(canvas);
			foreach (var geom in ToolCollection.Geometries) 
			{
				GeometryRenderer.Render(canvas, geom);
			}
			//TODO Try to do this on demand, rather than every draw...
			// Snapshotting without encodeing is rather fast though
			_snapShot = surface.Snapshot();
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

