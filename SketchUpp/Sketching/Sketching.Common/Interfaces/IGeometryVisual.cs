using Sketching.Views;

namespace Sketching.Interfaces
{
	public interface IGeometryVisual
	{
		/// <summary>
		/// Generic abstraction of a some kind of size
		/// Might be font size for text, line width for stroke, radius for point
		/// </summary>
		/// <value>The size.</value>
		double Size { get; set; }
		ToolPaletteItem SelectedItem { get; set; }
		bool IsFilled { get; set; }
		bool IsValid { get; }
		double MinSize { get; set; }
		double MaxSize { get; set; }
	}
}
