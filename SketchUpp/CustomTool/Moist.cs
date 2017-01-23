using System.Collections.Generic;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace SketchUpp.CustomTool
{
	public class Moist : IMoist
	{
		public Moist() : this(new ToolPaletteItem { ItemColor = Color.Black }, 60, false) { }
		public Moist(IGeometryVisual src) : this(src.SelectedItem, src.Size, src.IsFilled) { }
		public Moist(ToolPaletteItem selectedItem, double size, bool isFilled)
		{
			SelectedItem = selectedItem;
			IsFilled = isFilled;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 20;
			MaxSize = 120;
		}

		public ToolPaletteItem SelectedItem { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid => Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }
		public double Size { get; set; }
	}
}
