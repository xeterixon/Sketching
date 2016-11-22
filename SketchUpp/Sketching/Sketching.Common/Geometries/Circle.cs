using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Circle : ICircle
	{
		public Circle()
		{
			Color = Xamarin.Forms.Color.Fuchsia;
			Start = new Point(-1,-1);
			End = new Point(-1, -1);
			Size = 4;
		}
		public Circle(ICircle src):this()
		{
			Color = src.Color;
			Size = src.Size;
		}
		public Color Color { get; set; }
		public Point End { get; set; }
		public bool IsValid { get { return Start.X > 0 && End.X > 0; } }

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
