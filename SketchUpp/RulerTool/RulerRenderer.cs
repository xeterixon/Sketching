using System;
using Sketching.Extensions;
using Sketching.Helper;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace SketchUpp.RulerTool
{
	public class RulerRenderer : IGeometryRenderer
	{
		private const float TickLength = 10; // Length of the arrow lines

		public Type GeometryType =>typeof(IRuler);
		private double DegreeToRadian(double angle)
		{
			return Math.PI * angle / 180.0;
		}

		public void Render(SKCanvas canvas, IGeometryVisual geometry, double scale = 1)
		{
			var ruler = geometry as IRuler;
			if (ruler == null || !ruler.IsValid) return;
			using (var paint = new SKPaint()) 
			{
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)(ruler.Size * scale);
				paint.IsAntialias = true;
				var start = Converter.ToSKPoint(ruler.Start, scale);
				var stop = Converter.ToSKPoint(ruler.End, scale);
				paint.Color = ruler.Color.ToSkiaColor();
				var x1 = start.X;
				var y1 = start.Y;
				var x2 = stop.X;
				var y2 = stop.Y;

				canvas.DrawLine(x1, y1, x2, y2, paint);
				var angle = Math.Atan2(y2 - y1, x2 - x1);
				var x3 = (float)(x1 - TickLength * Math.Cos(angle - Math.PI / 2));
				var y3 = (float)(y1 - TickLength * Math.Sin(angle - Math.PI / 2));
				var x4 = (float)(x1 - TickLength * Math.Cos(angle + Math.PI / 2));
				var y4 = (float)(y1 - TickLength * Math.Sin(angle + Math.PI / 2));
					
				var x5 = (float)(x2 - TickLength * Math.Cos(angle - Math.PI / 2));
				var y5 = (float)(y2 - TickLength * Math.Sin(angle - Math.PI / 2));
				var x6 = (float)(x2 - TickLength * Math.Cos(angle + Math.PI / 2));
				var y6 = (float)(y2 - TickLength * Math.Sin(angle + Math.PI / 2));

				canvas.DrawLine(x1, y1, x3, y3, paint);
				canvas.DrawLine(x1, y1, x4, y4, paint);

				canvas.DrawLine(x2, y2, x5, y5, paint);
				canvas.DrawLine(x2, y2, x6, y6, paint);
			}
		}
	}
}
