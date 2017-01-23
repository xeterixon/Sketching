using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Text
{
	public class Text : IText
	{
		public Text() : this(new ToolPaletteItem { ItemColor = Color.Black }, 75, false) { }
		public Text(IGeometryVisual src) : this(src.SelectedItem, src.Size, src.IsFilled) { }
		public Text(ToolPaletteItem selectedItem, double size, bool isFilled)
		{
			Value = string.Empty;
			SelectedItem = selectedItem;
			IsFilled = isFilled;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 20;
			MaxSize = 200;
		}

		public ToolPaletteItem SelectedItem { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid => !string.IsNullOrEmpty(Value) && Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }
		public double Size { get; set; }
		public string Value { get; set; }
		public bool RoundedFill { get; set; }
	}
}
