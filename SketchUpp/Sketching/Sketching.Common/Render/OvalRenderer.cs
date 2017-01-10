﻿using System;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public class OvalRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IOval);

		public void Render(SKCanvas canvas, IGeometryVisual geometry, double scale)
		{
			var oval = geometry as IOval;
			using (var paint = new SKPaint())
			{
				if (oval == null) return;
				paint.Color = oval.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)(oval.Size * scale);

				canvas.DrawOval((float)(oval.Start.X * scale), (float)(oval.Start.Y * scale), (float)(oval.End.X - oval.Start.X) * (float)scale, (float)(oval.End.Y - oval.Start.Y) * (float)scale, paint);
			}
		}
	}
}
