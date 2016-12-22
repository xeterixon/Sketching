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

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale )
		{
			var s = gemoetry as IStroke;
			if (!s.IsValid) return;
			using (var paint = new SKPaint()) {
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)(s.Size * scale);
				paint.IsAntialias = true;
				paint.Color = s.Color.ToSkiaColor();
				var points = s.Points.Select( (arg) => Helper.Converter.ToSKPoint(arg,scale)).ToArray();
				canvas.DrawPoints(SKPointMode.Polygon, points, paint);
			}
		}
	}
}
