using System;
using Sketching.Extensions;
using Sketching.Helper;
using Sketching.Interfaces;
using Sketching.Renderer;
using SkiaSharp;

namespace SketchUpp.CustomTool
{
	public class MoistRenderer : IGeometryRenderer
	{
		public Type GeometryType => typeof(IMoist);

		public void Render(SKCanvas canvas, IGeometryVisual geometry, double scale)
		{
			var moist = geometry as IMoist;
			if (moist == null || !moist.IsValid) return;
			using (var paint = new SKPaint())
			{
				// Setup common paint parameters
				paint.Color = moist.SelectedItem.ItemColor.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				var text = moist.SelectedItem.ItemText;
				if (string.IsNullOrEmpty(text))
				{
					paint.StrokeWidth = (float)(moist.Size * scale);
					paint.StrokeCap = SKStrokeCap.Round;
					canvas.DrawPoint((float)(moist.Point.X * scale), (float)(moist.Point.Y * scale), paint);
				}
				else
				{
					paint.TextSize = (float)(moist.Size * scale);
					// Get the path of the text to calibrate the touch point to the middle of the text area
					var originalTextPath = paint.GetTextPath(text, (float)(moist.Point.X * scale), (float)(moist.Point.Y * scale));
					// Set the mid of the text area to the touch point
					var calibratedX = originalTextPath.Bounds.Left - originalTextPath.Bounds.Width / 2;
					var calibratedY = originalTextPath.Bounds.Bottom + originalTextPath.Bounds.Height / 2;

					// Set the right text color
					var textColor = Converter.ContrastColor(moist.SelectedItem.ItemColor);
					// Get the path of the calibrated text area and add some padding to the rect
					var calibratedTextPath = paint.GetTextPath(text, calibratedX, calibratedY);
					// Rect parameters
					float left;
					float top;
					float right;
					float bottom;
					float cornerRadius;

					var textSizeDependentPadding = paint.TextSize / 4;
					left = calibratedTextPath.Bounds.Left - textSizeDependentPadding;
					top = calibratedTextPath.Bounds.Top - textSizeDependentPadding;
					right = calibratedTextPath.Bounds.Right + textSizeDependentPadding;
					bottom = calibratedTextPath.Bounds.Bottom + textSizeDependentPadding;
					cornerRadius = paint.TextSize / 2;

					var rect = new SKRect(left, top, right, bottom);
					// Draw background rect
					canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);
					// Draw text
					paint.Color = textColor.ToSkiaColor();
					canvas.DrawText(text, calibratedX, calibratedY, paint);
					// Draw the background border in same color as text
					paint.IsStroke = true;
					canvas.DrawRoundRect(rect, cornerRadius, cornerRadius, paint);
				}
			}
		}
	}
}
