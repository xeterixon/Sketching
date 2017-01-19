using System;
using System.Collections.Generic;
using Sketching.Interfaces;
using Sketching.Tool;
using Xamarin.Forms;

namespace SketchUpp.CustomTool
{
	public class OvalTool : ITool<IOval>
	{

		public bool Active { get; set; }

		public IOval Geometry { get; set; } = new Oval { Size = 8, Color = Color.Maroon };
		public bool CanUseFill { get; set; }

		public string Name { get; set; } = "OvalTest";

		IGeometryVisual ITool.Geometry
		{
			get
			{
				return Geometry;
			}

			set
			{
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

		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
	}

}
