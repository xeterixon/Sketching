﻿using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class FilledRectangle : IFilledRectangle
	{
		public FilledRectangle() : this(Color.Black, 8) { }
		public FilledRectangle(IGeometryVisual src) : this(src.Color, src.Size) { }
		public FilledRectangle(Color color, double size)
		{
			Color = color;
			FillColor = color.ToFillColor();
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}
		public bool IsValid { get { return Start.X > 0 && End.X > 0; } }
		public double MinSize { get; set; }
		public double MaxSize { get; set; }

		public Color Color { get; set; }
		public Color FillColor { get; set; }
		public Point End { get; set; }

		public double Size { get; set; }
		public Point Start { get; set; }
	}
}
