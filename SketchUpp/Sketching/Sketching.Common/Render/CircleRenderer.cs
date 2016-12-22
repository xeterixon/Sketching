using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class CircleRenderer : IGeometryRenderer
	{
		public Type GeometryType {
			get {
				return typeof(ICircle);
			}
		}

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale )
		{
			var circle = gemoetry as ICircle;
			using (var paint = new SKPaint()) {
				paint.Color = circle.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float) (circle.Size * scale);
				canvas.DrawCircle((float)(circle.Start.X*scale), (float)(circle.Start.Y *scale), (float)(circle.Radius*scale), paint);
			}
			
		}
	}
}
