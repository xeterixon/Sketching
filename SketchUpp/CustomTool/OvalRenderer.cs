using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace SketchUpp.CustomTool
{
	public class OvalRenderer : IGeometryRenderer
	{
		public OvalRenderer()
		{
		}

		public Type GeometryType {
			get {
				return typeof(IOval);
			}
		}

		public void Render(SKCanvas canvas, IGeometryVisual geometry)
		{
			var oval = geometry as IOval;
			using (var paint = new SKPaint()) {
				paint.Color = oval.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)oval.Size;

				canvas.DrawOval((float)oval.Start.X, (float)oval.Start.Y, (float)(oval.End.X - oval.Start.X), (float)(oval.End.Y - oval.Start.Y), paint);
			}
		}
	}
}
