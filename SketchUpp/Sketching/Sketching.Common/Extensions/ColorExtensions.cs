using Xamarin.Forms;

namespace Sketching.Extensions
{
	public static class ColorExtensions
	{
		public static Color ToFillColor(this Color color)
		{
			var fillColor = new Color(color.R, color.G, color.B, 0.5);
			return fillColor;
		}
	}
}
