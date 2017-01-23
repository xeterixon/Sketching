using System;
using Sketching.Extensions;
using Sketching.Helper;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace Sketching.Tool.Text
{
	public class TextRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IText);
		private const int DefaultPadding = 10;

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
				paint.Color = text.SelectedItem.ItemColor.ToSkiaColor();

				// Get the path of the text to calibrate the touch point to the middle of the text area
				var originalTextPath = paint.GetTextPath(text.Value, (float)(text.Point.X * scale), (float)(text.Point.Y * scale));
				// Set the mid of the text area to the touch point
				var calibratedX = originalTextPath.Bounds.Left - originalTextPath.Bounds.Width / 2;
				var calibratedY = originalTextPath.Bounds.Bottom + originalTextPath.Bounds.Height / 2;

				if (text.IsFilled)
				{
					// Set the right text color
					var textColor = Converter.ContrastColor(text.SelectedItem.ItemColor);
					// Get the path of the calibrated text area and add some padding to the rect
					var calibratedTextPath = paint.GetTextPath(text.Value, calibratedX, calibratedY);
					// Rect parameters
					float left;
					float top;
					float right;
					float bottom;
					float cornerRadius = 0;
					if (text.RoundedFill)
					{
						var textSizeDependentPadding = paint.TextSize / 4;
						left = calibratedTextPath.Bounds.Left - textSizeDependentPadding;
						top = calibratedTextPath.Bounds.Top - textSizeDependentPadding;
						right = calibratedTextPath.Bounds.Right + textSizeDependentPadding;
						bottom = calibratedTextPath.Bounds.Bottom + textSizeDependentPadding;
						cornerRadius = paint.TextSize / 2;
					}
					else
					{
						left = calibratedTextPath.Bounds.Left - DefaultPadding;
						top = calibratedTextPath.Bounds.Top - DefaultPadding;
						right = calibratedTextPath.Bounds.Right + DefaultPadding;
						bottom = calibratedTextPath.Bounds.Bottom + DefaultPadding;
					}
					var rect = new SKRect(left, top, right, bottom);
					// Draw background rect
					canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);
					// Draw text
					paint.Color = textColor.ToSkiaColor();
					canvas.DrawText(text.Value, calibratedX, calibratedY, paint);
					// Draw the background border in same color as text
					paint.IsStroke = true;
					canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);
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
