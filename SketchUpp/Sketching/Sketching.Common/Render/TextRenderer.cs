using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class TextRenderer : IRenderer
	{
		public Type GeometryType {
			get {
				return typeof(IText);
			}
		}

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry)
		{
			var text = gemoetry as IText;
			using (var paint = new SKPaint()) {
				paint.Color = text.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.TextSize = (float)text.Size;
				canvas.DrawText(text.Value, (float)text.Point.X, (float)text.Point.Y, paint);
			}

		}
	}
}
