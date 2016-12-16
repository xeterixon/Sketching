using System.Collections.Generic;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Stroke : IStroke
	{
		public Stroke():this(Color.Black, 8) { }
		public Stroke(IGeometryVisual src) : this(src.Color, src.Size){}
		public Stroke(Color color, double size)
		{
			Color = color;
			Size = size;
		}

		public double Size { get; set; }
		public Color Color { get; set; }
		public List<Point> Points { get; set; } = new List<Point>();
		public bool IsValid => Points.Count > 0;
		public bool IsHighlighter { get; set; }
	}	
}
