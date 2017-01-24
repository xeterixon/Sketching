using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Mark
{
	public class Mark : IMark
	{
		public Mark() : this(new ToolSettings { SelectedColor = Color.Black }, 40, false) { }
		public Mark(IGeometryVisual src) : this(src.ToolSettings, src.Size, src.IsFilled) { }
		public Mark(ToolSettings toolSettings, double size, bool isFilled)
		{
			ToolSettings = toolSettings;
			IsFilled = isFilled;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 10;
			MaxSize = 70;
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
