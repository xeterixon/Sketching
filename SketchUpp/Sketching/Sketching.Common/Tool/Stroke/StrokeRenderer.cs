﻿using System;
using System.Linq;
using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Stroke
{
	public class StrokeRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IStroke);

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var stroke = gemoetry as IStroke;
			if (stroke == null || !stroke.IsValid) return;
			using (var paint = new SKPaint())
			{
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)(stroke.Size * scale);
				paint.IsAntialias = true;
				paint.Color = stroke.ToolSettings.SelectedColor.ToSkiaColor();

				var points = stroke.Points.Select(arg => Helper.Converter.ToSKPoint(arg, scale)).ToArray();
				// Draw HighLight or Stroke
				if (stroke.HighLight)
				{
					paint.StrokeCap = SKStrokeCap.Butt;
					if (stroke.IsStenciled)
					{
						using (var shader = ShaderFactory.Line(stroke.ToolSettings.SelectedColor.ToSkiaColor()))
						{
							paint.Shader = shader;
						}
					}
					paint.Color = stroke.ToolSettings.SelectedColor.ToFillColor().ToSkiaColor();
					canvas.DrawPointsInPath(paint, points);
					return;
				}
				else
				{
					canvas.DrawPoints(SKPointMode.Polygon, points, paint);
				}
				if (!points.Any()) return;
				paint.Color = stroke.ToolSettings.SelectedColor.ToFillColor().ToSkiaColor();
				paint.IsStroke = false;
				if (stroke.IsFilled) 
				{
					canvas.DrawPointsInPath(paint, points);

				}
				if (stroke.IsStenciled)
				{
					using (var shader = ShaderFactory.Line(stroke.ToolSettings.SelectedColor.ToSkiaColor()))
					{
						paint.Shader = shader;
					}
					canvas.DrawPointsInPath(paint, points);
				}
			}
		}
	}
}
