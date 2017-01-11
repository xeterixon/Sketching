using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class RectangleTool : IRectangleTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IRectangle Geometry { get; set; } = new Geometries.Rectangle();
		public bool CanUseFill { get; set; } = true;

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

		public RectangleTool()
		{
			Name = "Rectangle";
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Geometry = new Geometries.Rectangle(Geometry);
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
