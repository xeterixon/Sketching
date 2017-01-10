using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class FilledRectangleRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IFilledRectangle);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var rect = gemoetry as IFilledRectangle;
			using (var paint = new SKPaint())
			{
				if (rect == null) return;
				// Draw Rect
				paint.Color = rect.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)(rect.Size * scale);
				canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
				// Fill Rect
				paint.Color = rect.Color.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
			}
		}
	}
}
