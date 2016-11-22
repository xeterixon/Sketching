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
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SketchView.Abstractions.Graphics.Stroke"/> class.
		/// Note that the points are not copied, only Color and Size.
		/// </summary>
		/// <param name="s">S</param>
		public Stroke(IStroke s) : this()
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
