using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Text : IText
	{
		public Text() : this(Color.Black, 20) { }
		public Text(IGeometryVisual src) : this(src.Color, src.Size){}
		public Text(Color color, double size)
		{
			Value = string.Empty;
			Color = color;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 10;
			MaxSize = 40;
		}

		public Color Color { get; set; }
		public bool IsValid => !string.IsNullOrEmpty(Value) && Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }

		public Point Point { get; set; }

		public double Size { get; set; }
		public string Value { get; set; }
	}
}
