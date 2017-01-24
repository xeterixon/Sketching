using System;
using System.Linq;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Stroke
{
	public class StrokeRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IStroke);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var stroke = gemoetry as IStroke;
			if (stroke == null || !stroke.IsValid) return;
			using (var paint = new SKPaint())
			{
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)(stroke.Size * scale);
				paint.IsAntialias = true;
				paint.Color = stroke.SelectedItem.ItemColor.ToSkiaColor();

				var points = stroke.Points.Select(arg => Helper.Converter.ToSKPoint(arg, scale)).ToArray();
				// Draw HighLight or Stroke
				if (stroke.HighLight)
				{
					paint.StrokeCap = SKStrokeCap.Butt;
					paint.Color = stroke.SelectedItem.ItemColor.ToFillColor().ToSkiaColor();
					canvas.DrawPointsInPath(paint, points);
				}
				else
				{
					canvas.DrawPoints(SKPointMode.Polygon, points, paint);
				}
				// Fill Stroke
				if (!stroke.IsFilled) return;
				if (!points.Any()) return;
				paint.Color = stroke.SelectedItem.ItemColor.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				canvas.DrawPointsInPath(paint, points);
			}
		}
	}
}
