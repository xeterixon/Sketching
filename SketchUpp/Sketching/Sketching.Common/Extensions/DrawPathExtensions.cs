using System.Linq;
using SkiaSharp;

namespace Sketching.Extensions
{
	public static class DrawPathExtensions
	{
		public static void DrawPointsInPath(this SKCanvas canvas, SKPaint paint, SKPoint[] points)
		{
			var path = new SKPath();
			foreach (var skPoint in points)
			{
				if (!path.Points.Any())
					path.MoveTo(skPoint);
				path.LineTo(skPoint);
			}
			canvas.DrawPath(path, paint);
		}
	}
}
