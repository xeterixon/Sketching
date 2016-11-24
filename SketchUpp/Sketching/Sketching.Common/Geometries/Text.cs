using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Text : IText
	{
		public Text()
		{
			Value = string.Empty;
			Color = Xamarin.Forms.Color.Maroon;
			Size = 20;
			Point = new Point(-1, -1);
		}
		public Text(IGeometryVisual src) 
		{
			Color = src.Color;
			Size = src.Size;
		}

		public Color Color { get; set; }
		public bool IsValid {
			get {
				return !string.IsNullOrEmpty(Value) && Point.X > 0;
			}
		}

		public Point Point { get; set; }

		public double Size { get; set; }
		public string Value { get; set; }
	}
}
