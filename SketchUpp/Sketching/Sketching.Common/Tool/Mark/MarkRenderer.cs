using System;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Mark
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
