using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace SketchUpp.CustomTool
{
	public class Moist : IMoist
	{
		public Moist() : this(new ToolSettings { SelectedColor = Color.Black }, 60, false) { }
		public Moist(IGeometryVisual src) : this(src.ToolSettings, src.Size, src.IsFilled) { }
		public Moist(ToolSettings toolSettings, double size, bool isFilled)
		{
			ToolSettings = toolSettings;
			IsFilled = isFilled;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 20;
			MaxSize = 120;
		}

		public ToolSettings ToolSettings { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid => Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }
		public double Size { get; set; }
	}
}
