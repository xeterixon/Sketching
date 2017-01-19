using System;
using Sketching.Extensions;
using Sketching.Interfaces;
using SkiaSharp;

namespace Sketching.Tool.Arrow
{
	public class ArrowRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IArrow);

		private const int Angle = 50; // Angle of the arrow lines
		private const float L2 = 20; // Length of the arrow lines

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var arrow = gemoetry as IArrow;
			if (arrow == null || !arrow.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Paint parameters
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)(arrow.Size * scale);
				paint.IsAntialias = true;
				paint.Color = arrow.Color.ToSkiaColor();
				// Line points
				var startPoint = arrow.Start.ToSkiaPoint();
				var endPoint = arrow.End.ToSkiaPoint();
				var x1 = startPoint.X;
				var y1 = startPoint.Y;
				var x2 = endPoint.X;
				var y2 = endPoint.Y;
				// Calculate the arrow points
				var l1 = (float)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
				var x3 = (float)(x2 + L2 / l1 * ((x1 - x2) * Math.Cos(Angle) + (y1 - y2) * Math.Sin(Angle)));
				var y3 = (float)(y2 + L2 / l1 * ((y1 - y2) * Math.Cos(Angle) - (x1 - x2) * Math.Sin(Angle)));
				var x4 = (float)(x2 + L2 / l1 * ((x1 - x2) * Math.Cos(Angle) - (y1 - y2) * Math.Sin(Angle)));
				var y4 = (float)(y2 + L2 / l1 * ((y1 - y2) * Math.Cos(Angle) + (x1 - x2) * Math.Sin(Angle)));
				// Draw line
				canvas.DrawLine(x1, y1, x2, y2, paint);
				// Draw arrow lines
				canvas.DrawLine(x2, y2, x3, y3, paint);
				canvas.DrawLine(x2, y2, x4, y4, paint);
			}
		}
	}
}
