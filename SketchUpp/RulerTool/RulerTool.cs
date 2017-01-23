using System;
using System.Collections.Generic;
using Sketching.Interfaces;
using Sketching.Tool;
using Xamarin.Forms;

namespace SketchUpp.RulerTool
{
	public class RulerTool : ITool<IRuler>
	{
		public RulerTool()
		{
			Geometry = new Ruler();

		}

		public bool Active { get; set;}

		public bool CanUseFill {get; set; }

		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors {get; set; }

		public string CustomToolbarName {get; set; }

		public IRuler Geometry {get; set; }

		public string Name {get; set; }

		IGeometryVisual ITool.Geometry {
			get {
				return Geometry;
			}

			set {
				throw new NotSupportedException();
			}
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Geometry = new Ruler(Geometry);

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
