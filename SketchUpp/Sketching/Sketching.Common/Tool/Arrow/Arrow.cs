using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Arrow
{
	public class Arrow : IArrow
	{
		public Arrow() : this(Color.Black, 8, false) { }
		public Arrow(IGeometryVisual src) : this(src.Color, src.Size, src.IsFilled) { }
		public Arrow(Color color, double size, bool isFilled)
		{
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			Color = color;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public bool IsValid => Start.X > 0 && End.X > 0 && Start != End;
		public double Size { get; set; }
		public Color Color { get; set; }
		public bool IsFilled { get; set; }
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Start { get; set; }
		public Point End { get; set; }
	}
}
