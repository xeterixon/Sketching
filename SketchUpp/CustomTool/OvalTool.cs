using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace SketchUpp.CustomTool
{
	public class OvalTool : ITool<IOval>
	{

		public bool Active { get; set; }

		public IOval Geometry { get; set; } = new Oval { Size = 8, Color = Color.Maroon};
		public bool CanUseFill { get; set; }

		public string Name { get; set; } = "Oval";

		IGeometryVisual ITool.Geometry {
			get {
				return Geometry;
			}

			set {
				throw new NotImplementedException();
			}
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Geometry = new Oval(Geometry);
		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}

		public void TouchStart(Point p)
		{
			Geometry.Start = p;
		}
	}

}
