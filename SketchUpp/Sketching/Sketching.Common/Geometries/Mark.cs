using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Geometries
{
	public class Mark : IMark
	{
		public Mark() { }
		public Mark(IMark src)
		{
			this.Color = src.Color;
			this.Size = src.Size;
		}
		public Color Color { get; set; } = Xamarin.Forms.Color.FromRgba(0, 255, 0, 126);
		public bool IsValid {get {return true;}}
		public Point Point { get; set; }

		public double Size { get; set; } = 50.0;
	}
}
