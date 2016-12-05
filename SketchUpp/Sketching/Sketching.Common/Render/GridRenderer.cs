using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using SkiaSharp;
using Xamarin.Forms;

namespace Sketching.Common.Render
{
	public class GridRenderer
	{
		private List<Stroke> strokes = new List<Stroke>();
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
		public Xamarin.Forms.Color LineColor =  new Color(0, 0, 0, 0.4);
		public GridRenderer()
		{
			LineWidth = 1;
		}
		public void SetupGrid(SKCanvas canvas) 
		{
			strokes.Clear();
			// try to get roughly 15 vertical lines in portrait, rounding to the nearest 10 pixel
			if (Config.GridSize < 0) {
				var theLength = Math.Min(canvas.ClipBounds.Width, canvas.ClipBounds.Height);
				Config.GridSize = ((((int)theLength / 15) + 5) / 10) * 10;
			}
			var baseStroke = new Stroke { Size = LineWidth, Color = LineColor };
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

		}
		public void DrawGrid(SKCanvas canvas) 
		{
			//TODO Look into double buffer this so we don't have to draw it every time.
			foreach (var stroke in strokes) {
				GeometryRenderer.Render(canvas, stroke);
			}
			
		}
	}
}
