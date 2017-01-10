using System;
using System.Linq;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
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
				paint.IsStroke = true;
				paint.Color = stroke.Color.ToSkiaColor();
				var points = stroke.Points.Select(arg => Helper.Converter.ToSKPoint(arg, scale)).ToArray();
				canvas.DrawPoints(SKPointMode.Polygon, points, paint);
				// Fill Stroke
				//if (!stroke.IsFilled) return;
				//paint.Color = stroke.Color.ToFillColor().ToSkiaColor();
				//paint.IsStroke = false;
				//canvas.DrawPoints(SKPointMode.Polygon, points, paint);
			}
		}
	}
}
