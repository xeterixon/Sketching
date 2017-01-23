using Sketching.Interfaces;
using Xamarin.Forms;
namespace SketchUpp.RulerTool
{
	public class Ruler : IRuler
	{
		public Ruler()
		{
		}
		public Ruler(IGeometryVisual v) 
		{
			if (v != null) 
			{
				Color = v.Color;
				IsFilled = v.IsFilled;
				MaxSize = v.MaxSize;
				MinSize = v.MinSize;
				Size = v.Size;
			}
		}
		public Color Color { get; set; } = Color.Black;

		public Point End { get; set; }

		public bool IsFilled { get; set; } = false;

		public bool IsValid { get { return Start.X > 0 && End.X > 0;} }

		public double MaxSize { get; set; } = 20;

		public double MinSize { get; set; } = 1;

		public double Size { get; set; } = 5;

		public Point Start { get; set; }

	}
}
