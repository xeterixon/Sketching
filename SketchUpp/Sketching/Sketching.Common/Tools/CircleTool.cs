using System;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CircleTool : ICircleTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public ICircle Geometry { get; set; } = new Circle();
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

		public CircleTool() : this("Circle", 8) { }
		public CircleTool(string name, double size)
		{
			Name = name;
			Geometry.Size = size;
		}

		public void TouchStart(Point p)
		{
			Geometry.Start = p;
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			ReserGeometry();
		}

		private void ReserGeometry()
		{
			Geometry = new Circle(Geometry);
		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}
	}
}
