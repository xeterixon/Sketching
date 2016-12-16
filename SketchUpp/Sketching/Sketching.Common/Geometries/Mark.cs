﻿using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Mark : IMark
	{
		public Mark():this(Color.Black, 50) { }
		public Mark(IGeometryVisual src) : this(src.Color, src.Size){}
		public Mark(Color color, double size)
		{
			Color = color;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public Color Color { get; set; }
		public bool IsValid => true;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }

		public double Size { get; set; }
	}
}
