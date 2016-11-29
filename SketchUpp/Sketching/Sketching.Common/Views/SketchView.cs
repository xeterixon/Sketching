using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Sketching.Common.Render;
using Sketching.Common.Tools;
using SkiaSharp;
using Xamarin.Forms;
namespace Sketching.Common.Views
{

	public class SketchView : View, ITouchDelegate, ISketchView
	{
		//TODO Is this a bit overly designed? We only got one delegate and thats the ToolCollection object....
		public List<ITouchDelegate> Delegates { get; set; } = new List<ITouchDelegate>();
		private IToolCollection _tools;
		private SKSurface _surface;
		private byte[] _imageData;
		public IToolCollection ToolCollection {
			get { return _tools; }
			set {
				_tools = value;
				_tools.View = this;
			}
		}
		private int _lastCanvasWidth = -1;
		public byte[] ImageData()
		{
			return _imageData;
		}
		public Action<CallbackType> CallbackToNative { get; set; }

		public virtual void Draw(SKSurface surface, SKImageInfo info)
		{
			_surface = surface;
			if (_lastCanvasWidth != (int)surface.Canvas.ClipBounds.Width) {
				_lastCanvasWidth = (int)surface.Canvas.ClipBounds.Width;
				SetupGrid(surface.Canvas);
			}
			surface.Canvas.Clear(SKColors.AliceBlue);
			var canvas = surface.Canvas;
			if (ToolCollection == null) return;
			DrawGrid(canvas);
			//This is really not that object oriented... So kill me...
			foreach (var geom in ToolCollection.Geometries) {
				if (!geom.IsValid) continue;
				GeometryRenderer.Render(canvas, geom);
			}
			//TODO Try to do this on demand, rather than very draw...
			_imageData = surface.Snapshot().Encode(SKImageEncodeFormat.Jpeg, 100).ToArray();
		}

		private void SetupGrid(SKCanvas canvas)
		{
			//NOTE The canvas arg is not used yet....
			grid.Clear();
			// try to get roughly 15 vertical lines in portrait, rounding to the nearest 10 pixel
			var theLength = Math.Min(canvas.ClipBounds.Width, canvas.ClipBounds.Height);
			if (Config.GridSize < 0) {
				Config.GridSize = ((((int)theLength / 15) + 5) / 10) * 10;
			}
			var baseStroke = new Stroke { Size = 1, Color = new Color(0, 0, 0, 0.4) };
			int counter = 0;
			do {
				var stroke = new Stroke(baseStroke);
				stroke.Points.Add(new Point { X = counter * Config.GridSize, Y = 0 });
				stroke.Points.Add(new Point { X = counter * Config.GridSize, Y = canvas.ClipBounds.Height });
				grid.Add(stroke);
				counter++;
			} while ((counter * Config.GridSize) < canvas.ClipBounds.Width);
			counter = 0;
			do {
				var stroke = new Stroke(baseStroke);
				stroke.Points.Add(new Point { X = 0, Y = counter * Config.GridSize });
				stroke.Points.Add(new Point { X = canvas.ClipBounds.Width, Y = counter * Config.GridSize });
				grid.Add(stroke);
				counter++;

			} while (counter * Config.GridSize < canvas.ClipBounds.Height);
		}
		private List<Stroke> grid = new List<Stroke>();
		private void DrawGrid(SKCanvas canvas)
		{
			//TODO Look into double buffer this so we don't have to draw it every time.
			foreach (var stroke in grid) {
				GeometryRenderer.Render(canvas, stroke);
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

