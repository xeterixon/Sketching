using System;
using System.Collections.Generic;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using Sketching.Common.Render;
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
		private byte[] _imageData;
		private GridRenderer _gridRenderer = new GridRenderer();
		//TODO Should be a bindable property
		public Xamarin.Forms.Color CanvasBackgroundColor { get; set; } = Color.FromHex("#F0F8FF");
		public IImage BackgroundImage { get; set; }
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
		private int _lastCanvasWidth = -1;
		public byte[] ImageData()
		{
			return _imageData;
		}
		public Action<CallbackType> CallbackToNative { get; set; }

		public virtual void Draw(SKSurface surface, SKImageInfo info)
		{
			if (_lastCanvasWidth != (int)surface.Canvas.ClipBounds.Width) {
				_lastCanvasWidth = (int)surface.Canvas.ClipBounds.Width;
				_gridRenderer.SetupGrid(surface.Canvas);
			}
			var canvas = surface.Canvas;
			surface.Canvas.Clear(CanvasBackgroundColor.ToSkiaColor());
			DrawBackground(canvas);
			if (ToolCollection == null) return;
			_gridRenderer.DrawGrid(canvas);
			foreach (var geom in ToolCollection.Geometries) {
				if (!geom.IsValid) continue;
				GeometryRenderer.Render(canvas, geom);
			}
			//TODO Try to do this on demand, rather than every draw...
			_imageData = surface.Snapshot().Encode(SKImageEncodeFormat.Jpeg, 100).ToArray();
		}

		private void DrawBackground(SKCanvas canvas) 
		{
			if (BackgroundImage == null) return;
			using (var data = new SKData(BackgroundImage.Data)) 
			{
				using (var image = SKImage.FromData(data)) {
					var scale = Math.Min(canvas.ClipDeviceBounds.Width/(double)image.Width,  canvas.ClipDeviceBounds.Height/(double)image.Height);
					canvas.DrawImage(image,SKRect.Create((float)(image.Width * scale),(float)(image.Height*scale)));
				}
			}

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

