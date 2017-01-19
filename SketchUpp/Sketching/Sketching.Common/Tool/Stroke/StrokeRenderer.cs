using System;
using System.Linq;
using Sketching.Extensions;
using Sketching.Interfaces;
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
				// Draw Stroke
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)(stroke.Size * scale);
				paint.IsAntialias = true;
				paint.Color = stroke.Color.ToSkiaColor();
				if (stroke.HighLight) paint.BlendMode = SKBlendMode.Darken; //TODO: Maybe use filters, paint.ColorFilter = SKColorFilter.CreateLighting()
				var points = stroke.Points.Select(arg => Helper.Converter.ToSKPoint(arg, scale)).ToArray();
				canvas.DrawPoints(SKPointMode.Polygon, points, paint);
				// Fill Stroke
				if (!stroke.IsFilled) return;
				if (!points.Any()) return;
				paint.Color = stroke.Color.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				var path = new SKPath();
				foreach (var skPoint in points)
				{
					if (!path.Points.Any())
						path.MoveTo(skPoint);
					path.LineTo(skPoint);
				}
				canvas.DrawPath(path, paint);
			}
		}
	}
}
