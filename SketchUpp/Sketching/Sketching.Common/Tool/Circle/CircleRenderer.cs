using System;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Circle
{
	public class CircleRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(ICircle);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var circle = gemoetry as ICircle;
			if (circle == null || !circle.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Draw Circle
				paint.Color = circle.ToolSettings.SelectedColor.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)(circle.Size * scale);
				canvas.DrawCircle((float)(circle.Start.X * scale), (float)(circle.Start.Y * scale), (float)(circle.Radius * scale), paint);
				// Fill Circle
				paint.Color = circle.ToolSettings.SelectedColor.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				if (circle.IsFilled) 
				{
					canvas.DrawCircle((float)(circle.Start.X * scale), (float)(circle.Start.Y * scale), (float)(circle.Radius * scale), paint);
				}
				if (circle.IsStenciled)
				{
					using (var shader = ShaderFactory.Line(circle.ToolSettings.SelectedColor.ToSkiaColor()))
					{
						paint.Shader = shader;
					}
					canvas.DrawCircle((float)(circle.Start.X * scale), (float)(circle.Start.Y * scale), (float)(circle.Radius * scale), paint);
				}

			}
		}
	}
}
