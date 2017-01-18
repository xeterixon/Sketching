﻿using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Mark
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
			Point = new Point(-1, -1);
			MinSize = 10;
			MaxSize = 70;
		}

		public Color Color { get; set; }
		public bool IsFilled { get; set; }
		public bool IsValid => Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }
		public double Size { get; set; }
	}
}
