using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Rectangle : IRectangle
	{
		public Rectangle() : this(Color.Black, 8) { }
		public Rectangle(IGeometryVisual src) : this(src.Color, src.Size){}
		public Rectangle(Color color, double size) 
		{
			Color = color;
			Size = size;
		}
		public bool IsValid { get { return Start.X > 0 && End.X > 0; } }

		public Color Color { get; set; }
		public Point End { get; set; }

		public double Size { get; set; }
		public Point Start { get; set; }
	}
}
