using System;
using System.Collections.Generic;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Stroke : IStroke
	{
		public double Size { get; set; } = 2;
		public Color Color { get; set; } = Color.Black;
		public List<Point> Points { get; set; } = new List<Point>();
		public Stroke(IGeometryVisual s) : this()
		{
			Color = s.Color;
			Size = s.Size;
		}
		public bool IsValid { get { return Points.Count > 0;}}
		public Stroke(Xamarin.Forms.Color color, double size) 
		{
			Color = color;
			Size = size;
		}
		public Stroke(){}
	}	
}
