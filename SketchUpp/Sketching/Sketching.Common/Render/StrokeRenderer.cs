using System;
using System.Linq;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class StrokeRenderer : IGeometryRenderer
	{
		public Type GeometryType {
			get {
				return typeof(IStroke);
			}
		}

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry)
		{
			var s = gemoetry as IStroke;
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
	}
}
