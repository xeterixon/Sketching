using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class MarkRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IMark);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var mark = gemoetry as IMark;
			if (mark == null || !mark.IsValid) return;
			using (var paint = new SKPaint())
			{
				paint.Color = mark.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.StrokeWidth = (float)(mark.Size * scale);
				paint.StrokeCap = SKStrokeCap.Round;
				canvas.DrawPoint((float)(mark.Point.X * scale), (float)(mark.Point.Y * scale), paint);
				//TODO: Maybe fix real fill
			}
		}
	}
}
