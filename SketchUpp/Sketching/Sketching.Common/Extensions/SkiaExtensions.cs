using System;

using Xamarin.Forms;

namespace Sketching.Common.Extensions
{
	public static class SkiaExtensions
	{
		public static SkiaSharp.SKColor ToSkiaColor(this Xamarin.Forms.Color self) 
		{
			return new SkiaSharp.SKColor((byte)(self.R*255),(byte) (self.G* 255),(byte) (self.B* 255), (byte)(self.A* 255));
		}
		public static SkiaSharp.SKPoint ToSkiaPoint(this Point self) 
		{
			return new SkiaSharp.SKPoint((float)self.X, (float)self.Y);
		}
		public static SkiaSharp.SKPoint Convert(Point p) 
		{
			return p.ToSkiaPoint();
		}
	}
}

