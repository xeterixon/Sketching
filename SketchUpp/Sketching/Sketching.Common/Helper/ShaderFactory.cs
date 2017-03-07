using System;
using SkiaSharp;

namespace Sketching
{
	// This is POC, more or less.
	public static class ShaderFactory
	{
		public static SKShader Line(SKColor color) 
		{

			using (var bitmap = new SKBitmap(15, 15, true))
			{
				using (var canvas = new SKCanvas(bitmap))
				{
					canvas.Clear(SKColors.Transparent);
					using (var paint = new SKPaint())
					{
						paint.Color = color;
						paint.IsStroke = true;
						paint.IsAntialias = true;
						paint.StrokeWidth = 4;
						canvas.DrawLine(0, 0, bitmap.Width, 0, paint);
					}
				}

				// Close enough to 2*PI/8
				return SKShader.CreateBitmap(bitmap, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat,SKMatrix.MakeRotation(0.8f));
			}
		}
	}
}
