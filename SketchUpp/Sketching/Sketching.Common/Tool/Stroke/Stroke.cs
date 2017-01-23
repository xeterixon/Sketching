using System.Collections.Generic;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Stroke
{
	public class Stroke : IStroke
	{
		public Stroke() : this(new ToolPaletteItem { ItemColor = Color.Black }, 8, false) { }
		public Stroke(IGeometryVisual src) : this(src.SelectedItem, src.Size, src.IsFilled) { }
		public Stroke(ToolPaletteItem selectedItem, double size, bool isFilled)
		{
			SelectedItem = selectedItem;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public double Size { get; set; }
		public ToolPaletteItem SelectedItem { get; set; }
		public bool IsFilled { get; set; }
		public List<Point> Points { get; set; } = new List<Point>();
		public bool HighLight { get; set; } = false;
		public bool IsValid => Points.Count > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
	}
}
