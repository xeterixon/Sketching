using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Text
{
	public class Text : IText
	{
		public Text() : this(Color.Black, 75, false) { }
		public Text(IGeometryVisual src) : this(src.Color, src.Size, src.IsFilled) { }
		public Text(Color color, double size, bool isFilled)
		{
			Value = string.Empty;
			Color = color;
			IsFilled = isFilled;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 20;
			MaxSize = 200;
		}

		public Color Color { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid => !string.IsNullOrEmpty(Value) && Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }
		public double Size { get; set; }
		public string Value { get; set; }
	}
}
