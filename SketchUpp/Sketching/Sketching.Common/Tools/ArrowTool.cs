using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class ArrowTool : IArrowTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IArrow Geometry { get; set; } = new Geometries.Arrow();
		public bool CanUseFill { get; set; } = false;

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

		public ArrowTool()
		{
			Name = "Arrow";
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Geometry = new Geometries.Arrow(Geometry);
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
