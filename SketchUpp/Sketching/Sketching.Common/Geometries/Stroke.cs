using System.Collections.Generic;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Stroke : IStroke
	{
		public Stroke() : this(Color.Black, 8, false) { }
		public Stroke(IGeometryVisual src) : this(src.Color, src.Size, src.IsFilled) { }
		public Stroke(Color color, double size, bool isFilled)
		{
			Color = color;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public double Size { get; set; }
		public Color Color { get; set; }
		public bool IsFilled { get; set; }
		public List<Point> Points { get; set; } = new List<Point>();
		public bool HighLight { get; set; } = false;
		public bool IsValid => Points.Count > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
	}
}
