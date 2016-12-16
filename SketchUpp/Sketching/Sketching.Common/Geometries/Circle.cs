using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Circle : ICircle
	{
		public Circle() : this(Color.Black, 8) { }
		public Circle(IGeometryVisual src) : this(src.Color, src.Size) { }

		public Circle(Color color, double size)
		{
			Color = color;
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public Color Color { get; set; }
		public Point End { get; set; }
		public bool IsValid { get { return Start.X > 0 && End.X > 0; } }
		public double MinSize { get; set; }
		public double MaxSize { get; set; }

		public double Radius {
			get 
			{
				if (!IsValid) return 0.0;
				return Start.Distance(End);
			}
		}

		public double Size { get; set; }
		public Point Start { get; set; }
	}
}
