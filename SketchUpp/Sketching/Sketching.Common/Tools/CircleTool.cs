using System;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CircleTool : ICircleTool
	{
		public IGeometryVisual Geometry { get; set; } = new Circle();

		public string Name { get; set; }

		public ToolType ToolType { get; set; }

		public bool Active { get; set; }
		private ICircle Circle {get {return (ICircle)Geometry;}}
		public CircleTool()
		{
			Name = "Circle";
			ToolType = ToolType.Circle;
		}

		public void Activate()
		{
			Active = true;
		}
		public void Deactivate()
		{
			Active = false;
		}

		private void Init() 
		{
			Geometry = new Circle(Circle);
		}

		public void TouchStart(Point p)
		{
			Circle.Start = p;
		}

		public void TouchEnd(Point p)
		{
			Circle.End = p;
			Init();
		}

		public void TouchMove(Point p)
		{
			Circle.End = p;
		}
	}
}
