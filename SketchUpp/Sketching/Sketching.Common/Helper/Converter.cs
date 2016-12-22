using System;
using Sketching.Common.Extensions;
using Xamarin.Forms;

namespace Sketching.Common.Helper
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
	}
}
