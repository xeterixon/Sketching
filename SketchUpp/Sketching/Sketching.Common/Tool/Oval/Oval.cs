using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Oval
{
	public class Oval : IOval
	{
		public Oval() : this(new ToolSettings { SelectedColor = Color.Black }, 8, false) { }
		public Oval(IGeometryVisual src) : this(src.ToolSettings, src.Size, src.IsFilled) { }
		public Oval(ToolSettings toolSettings, double size, bool isFilled)
		{
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			ToolSettings = toolSettings;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public ToolSettings ToolSettings { get; set; }
		public bool IsFilled { get; set; }
		public Point End { get; set; }
		public bool IsValid => Start.X > 0 && End.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public double Size { get; set; }
		public Point Start { get; set; }
	}
}
