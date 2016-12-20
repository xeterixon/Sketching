using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IGeometryVisual
	{
		/// <summary>
		/// Generic abstraction of a some kind of size
		/// Might be font size for text, line widht for stroke, radius for point
		/// </summary>
		/// <value>The size.</value>
		double Size { get; set; }
		Color Color { get; set; }
		bool IsValid { get; }
		double MinSize { get; set; }
		double MaxSize { get; set; }
	}
}
