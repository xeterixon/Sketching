using Xamarin.Forms;

namespace Sketching.Extensions
{
	public static class SkiaExtensions
	{
		public static SkiaSharp.SKColor ToSkiaColor(this Color self) 
		{
			return new SkiaSharp.SKColor((byte)(self.R*255),(byte) (self.G* 255),(byte) (self.B* 255), (byte)(self.A* 255));
		}
		public static SkiaSharp.SKPoint ToSkiaPoint(this Point self, double scale) 
		{
			return new SkiaSharp.SKPoint((float)(self.X * scale),(float)( self.Y * scale));
		}
		public static SkiaSharp.SKPoint ToSkiaPoint(this Point self) 
		{
			return new SkiaSharp.SKPoint((float)self.X, (float)self.Y);
		}
	}
}

