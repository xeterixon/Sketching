using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class TextRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IText);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var text = gemoetry as IText;
			if (text == null || !text.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Draw Text
				paint.Color = text.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.TextSize = (float)(text.Size * scale);
				canvas.DrawText(text.Value, (float)(text.Point.X * scale), (float)(text.Point.Y * scale), paint);
				// Fill Text
				//TODO: Background Rect if IsFilled
			}
		}
	}
}
