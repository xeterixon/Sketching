using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Mark : IMark
	{
		public Mark() : this(Color.Black, 40, false) { }
		public Mark(IGeometryVisual src) : this(src.Color, src.Size, src.IsFilled) { }
		public Mark(Color color, double size, bool isFilled)
		{
			Color = color;
			IsFilled = isFilled;
			Size = size;
			MinSize = 10;
			MaxSize = 70;
		}

		public Color Color { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid => true;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }

		public double Size { get; set; }
	}
}
