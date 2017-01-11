using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace SketchUpp.CustomTool
{
	public class Oval : IOval
	{
		public Oval() 
		{
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			
		}
		public Oval(IGeometryVisual o) :base()
		{
			Color = o.Color;
			Size = o.Size;
		}
		public Color Color { get; set; }
		public bool IsFilled { get; set; }

		public Point End { get; set; }

		public bool IsValid {
			get {
				return Start.X > 0 && End.X > 0;
			}
		}

		public double MinSize { get; set; } = 1;
		public double MaxSize { get; set; } = 20;

		public double Size { get; set; } = 1;

		public Point Start {get;set;}
	}
}
