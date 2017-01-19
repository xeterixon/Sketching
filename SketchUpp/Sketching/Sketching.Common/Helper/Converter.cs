using Sketching.Extensions;
using Xamarin.Forms;

namespace Sketching.Helper
{
	public static class Converter
	{
		public static SkiaSharp.SKRect ToSKRect(Point p1, Point p2, double scale= 1.0) 
		{
			return new SkiaSharp.SKRect((float)(p1.X*scale), (float)(p1.Y*scale),(float)( p2.X*scale),(float)( p2.Y*scale));
		}
		public static SkiaSharp.SKPoint ToSKPoint(Point p, double scale = 1.0)
		{
			return p.ToSkiaPoint(scale);
		}
		public static Color ContrastColor(Color color) 
		{
			// Color components on a Xamarin.Forms.Color are doubles ranging from 0 to 1 not 0 to 255 as one is used to.
			double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
			if (a < 0.49) 
				return Color.Black;
			else
				return Color.White;

		}
	}
}
