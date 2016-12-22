using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class MarkRenderer : IGeometryRenderer
	{
		public Type GeometryType {
			get {
				return typeof(IMark);
			}
		}

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var mark = gemoetry as IMark;
			using (var paint = new SKPaint()) {
				paint.Color = mark.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.StrokeWidth = (float)(mark.Size * scale);
				paint.StrokeCap = SKStrokeCap.Round;
				canvas.DrawPoint((float)(mark.Point.X*scale), (float)(mark.Point.Y*scale), paint);

			}
			
		}
	}
}
