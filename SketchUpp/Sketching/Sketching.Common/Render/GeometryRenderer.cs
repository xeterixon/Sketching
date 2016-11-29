using System.Linq;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public static class GeometryRenderer
	{
		public static void Render(SKCanvas canvas, IGeometryVisual gemoetry)
		{
			if (gemoetry is IStroke) { DrawStroke(canvas, (IStroke)gemoetry); }
			if (gemoetry is ICircle) { DrawCircle(canvas, (ICircle)gemoetry); }
			if (gemoetry is IMark) { DrawMark(canvas, (IMark)gemoetry); }
			if (gemoetry is IRectangle) { DrawRectangle(canvas, (IRectangle)gemoetry); }
			if (gemoetry is IText) { DrawText(canvas, (IText)gemoetry); }

		}
		private static void DrawStroke(SkiaSharp.SKCanvas canvas, IStroke s)
		{
			// Got to have more than one point to draw a line
			if (!s.IsValid) return;
			using (var paint = new SKPaint()) {
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)s.Size;
				paint.IsAntialias = true;
				paint.Color = s.Color.ToSkiaColor();
				var first = s.Points.First();
				var theRest = s.Points.Skip(1);
				foreach (var point in theRest) {
					canvas.DrawLine((float)first.X, (float)first.Y, (float)point.X, (float)point.Y, paint);
					first = point;
				}
			}
		}
		private static void DrawText(SKCanvas canvas, IText text)
		{
			using (var paint = new SKPaint()) {
				paint.Color = text.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.TextSize = (float)text.Size;
				canvas.DrawText(text.Value, (float)text.Point.X, (float)text.Point.Y, paint);
			}
		}
		private static void DrawRectangle(SKCanvas canvas, IRectangle rect)
		{
			using (var paint = new SKPaint()) {
				paint.Color = rect.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)rect.Size;
				canvas.DrawRect(new SKRect((float)rect.Start.X, (float)rect.Start.Y, (float)rect.End.X, (float)rect.End.Y), paint);

			}
		}
		private static void DrawCircle(SKCanvas canvas, ICircle circle)
		{
			using (var paint = new SKPaint()) {
				paint.Color = circle.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)circle.Size;
				canvas.DrawCircle((float)circle.Start.X, (float)circle.Start.Y, (float)circle.Radius, paint);
			}
		}
		private static void DrawMark(SKCanvas canvas, IMark mark)
		{
			using (var paint = new SKPaint()) {
				paint.Color = mark.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.StrokeWidth = (float)mark.Size;
				paint.StrokeCap = SKStrokeCap.Round;
				canvas.DrawPoint((float)mark.Point.X, (float)mark.Point.Y, paint);

			}
		}
	}
}
