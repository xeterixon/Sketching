using System;
using System.Collections.Generic;
using Sketching.Tool.Stroke;
using SkiaSharp;
using Xamarin.Forms;
namespace Sketching.Renderer
{
	public class GridRenderer : IRenderer
	{
		private List<Stroke> strokes = new List<Stroke>();
		public bool Enabled { get; set; } = true;
		private double _lineWidth;
		public double LineWidth {
			get {
				return _lineWidth;
			}
			set {
				if (value < 1) return;
				_lineWidth = value;
			}
		}
		private int _lastCanvasWidth = -1;
		private SKPicture _gridPicture;
		public Color LineColor =  new Color(0, 0, 0, 0.4);
		public GridRenderer()
		{
			LineWidth = 1;
		}
		public void Setup(SKCanvas canvas, double scale) 
		{
			if (!Enabled) return;
			strokes.Clear();
			// try to get roughly 15 vertical lines in portrait, rounding to the nearest 10 pixel
//			if (Config.GridSize < 0) 
			{
				var theLength = Math.Min(canvas.ClipBounds.Width, canvas.ClipBounds.Height);
				Config.GridSize = ((((int)theLength / 15) + 5) / 10) * 10;
			}
			var baseStroke = new Stroke { Size = LineWidth * scale, Color = LineColor };
			int counter = 0;
			do {
				var stroke = new Stroke(baseStroke);
				stroke.Points.Add(new Point { X = counter * Config.GridSize, Y = 0 });
				stroke.Points.Add(new Point { X = counter * Config.GridSize, Y = canvas.ClipBounds.Height });
				strokes.Add(stroke);
				counter++;
			} while ((counter * Config.GridSize) < canvas.ClipBounds.Width);
			counter = 0;
			do {
				var stroke = new Stroke(baseStroke);
				stroke.Points.Add(new Point { X = 0, Y = counter * Config.GridSize });
				stroke.Points.Add(new Point { X = canvas.ClipBounds.Width, Y = counter * Config.GridSize });
				strokes.Add(stroke);
				counter++;

			} while (counter * Config.GridSize < canvas.ClipBounds.Height);
			// Drawing the grid to a picture.
			// A tad faster than drawing directly on the canvas. No big impact though
			using (var recorder = new SKPictureRecorder()) {
				recorder.BeginRecording(canvas.ClipBounds);
				DrawBackbuffer(recorder.RecordingCanvas);
				_gridPicture = recorder.EndRecording();
			}


		}
		private void DrawBackbuffer(SKCanvas c) 
		{
			foreach (var stroke in strokes) {
				
				GeometryRenderer.Render(c, stroke);
			}
		}
		public void Render(SKCanvas canvas, double scale) 
		{
			if (!Enabled) return;
			
			if (_lastCanvasWidth != (int)canvas.ClipBounds.Width) 
			{
				Setup(canvas, scale);
				_lastCanvasWidth = (int)canvas.ClipBounds.Width;
			}
			if (_gridPicture != null) 
			{
				canvas.DrawPicture(_gridPicture);
			}

		}
	}
}
