using System;
using Sketching.Extensions;
using Sketching.Helper;
using Sketching.Interfaces;
using SkiaSharp;
using Xamarin.Forms;

namespace Sketching.Tool.Text
{
	public class TextRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IText);
		private const int Padding = 10;

		public void Render(SKCanvas canvas, IGeometryVisual gemoetry, double scale)
		{
			var text = gemoetry as IText;
			if (text == null || !text.IsValid) return;

			using (var paint = new SKPaint())
			{
				// Setup common paint parameters
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.TextSize = (float)(text.Size * scale);
				paint.Color = text.Color.ToSkiaColor();

				// Get the path of the text to calibrate the touch point to the middle of the text area
				var originalTextPath = paint.GetTextPath(text.Value, (float)(text.Point.X * scale), (float)(text.Point.Y * scale));
				// Set the mid of the text area to the touch point
				var calibratedX = originalTextPath.Bounds.Left - originalTextPath.Bounds.Width / 2;
				var calibratedY = originalTextPath.Bounds.Bottom + originalTextPath.Bounds.Height / 2;

				if (text.IsFilled)
				{
					// Set the right text color
					var textColor = Converter.ContrastColor(text.Color);

					var calibratedTextPath = paint.GetTextPath(text.Value, calibratedX, calibratedY);
					// Get the path of the calibrated text area and add some padding to the rect
					var left = calibratedTextPath.Bounds.Left - Padding;
					var top = calibratedTextPath.Bounds.Top - Padding;
					var right = calibratedTextPath.Bounds.Right + Padding;
					var bottom = calibratedTextPath.Bounds.Bottom + Padding;
					var rect = new SKRect(left, top, right, bottom);
					// Draw background rect
					canvas.DrawRect(rect, paint);
					// Draw text
					paint.Color = textColor.ToSkiaColor();
					canvas.DrawText(text.Value, calibratedX, calibratedY, paint);
					// Draw the background border in same color as text
					paint.IsStroke = true;
					canvas.DrawRect(rect, paint);
				}
				else
				{
					// Draw Text
					canvas.DrawText(text.Value, calibratedX, calibratedY, paint);
				}
			}
		}
	}
}
