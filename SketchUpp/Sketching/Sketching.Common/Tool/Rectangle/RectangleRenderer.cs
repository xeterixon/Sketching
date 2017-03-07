using System;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Rectangle
{
	public class RectangleRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IRectangle);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var rect = gemoetry as IRectangle;
			if (rect == null || !rect.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Draw Rect
				paint.Color = rect.ToolSettings.SelectedColor.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)(rect.Size * scale);
				canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
				// Fill and or stencil rect
				paint.Color = rect.ToolSettings.SelectedColor.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				if (rect.IsFilled)
				{
					canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
				}
				if (rect.IsStenciled)
				{
					using (var shader = ShaderFactory.Line(rect.ToolSettings.SelectedColor.ToSkiaColor()))
					{
						paint.Shader = shader;
					}
					canvas.DrawRect(Helper.Converter.ToSKRect(rect.Start, rect.End, scale), paint);
				}
			}
		}
	}
}
