using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Rectangle
{
	public class Rectangle : IRectangle
	{
		public Rectangle() : this(new ToolPaletteItem { ItemColor = Color.Black }, 8, false) { }
		public Rectangle(IGeometryVisual src) : this(src.SelectedItem, src.Size, src.IsFilled) { }
		public Rectangle(ToolPaletteItem selectedItem, double size, bool isFilled)
		{
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			SelectedItem = selectedItem;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public ToolPaletteItem SelectedItem { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid { get { return Start.X > 0 && End.X > 0; } }
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point End { get; set; }
		public double Size { get; set; }
		public Point Start { get; set; }
	}
}
