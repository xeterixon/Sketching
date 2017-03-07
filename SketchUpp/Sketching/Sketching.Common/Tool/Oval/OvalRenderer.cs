using System;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Oval
{
	public class OvalRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IOval);

		public void Render(SKCanvas canvas, IGeometryVisual geometry, double scale)
		{
			var oval = geometry as IOval;
			if (oval == null || !oval.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Draw Oval
				paint.Color = oval.ToolSettings.SelectedColor.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)(oval.Size * scale);
				canvas.DrawOval((float)(oval.Start.X * scale), (float)(oval.Start.Y * scale), (float)(oval.End.X - oval.Start.X) * (float)scale, (float)(oval.End.Y - oval.Start.Y) * (float)scale, paint);
				// Fill Oval
				paint.Color = oval.ToolSettings.SelectedColor.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				if (oval.IsFilled) 
				{
					canvas.DrawOval((float)(oval.Start.X * scale), (float)(oval.Start.Y * scale), (float)(oval.End.X - oval.Start.X) * (float)scale, (float)(oval.End.Y - oval.Start.Y) * (float)scale, paint);

				}
				if (oval.IsStenciled)
				{
					using (var shader = ShaderFactory.Line(oval.ToolSettings.SelectedColor.ToSkiaColor()))
					{
						paint.Shader = shader;
					}
					canvas.DrawOval((float)(oval.Start.X * scale), (float)(oval.Start.Y * scale), (float)(oval.End.X - oval.Start.X) * (float)scale, (float)(oval.End.Y - oval.Start.Y) * (float)scale, paint);

				}
			}
		}
	}
}
