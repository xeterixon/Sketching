using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Oval : IOval
	{
		public Oval() : this(Color.Black, 8, false) { }
		public Oval(IGeometryVisual src) : this(src.Color, src.Size, src.IsFilled) { }
		public Oval(Color color, double size, bool isFilled)
		{
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			Color = color;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public Color Color { get; set; }
		public bool IsFilled { get; set; }
		public Point End { get; set; }
		public bool IsValid => Start.X > 0 && End.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public double Size { get; set; }
		public Point Start { get; set; }
	}
}
