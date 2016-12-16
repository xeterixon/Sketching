using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class RectangleRenderer : IRenderer
	{
		public Type GeometryType {
			get {
				return typeof(IRectangle);
			}
		}

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry)
		{
			var rect = gemoetry as IRectangle;
			using (var paint = new SKPaint()) {
				paint.Color = rect.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)rect.Size;
				canvas.DrawRect(new SKRect((float)rect.Start.X, (float)rect.Start.Y, (float)rect.End.X, (float)rect.End.Y), paint);
			}
		}
	}
}
