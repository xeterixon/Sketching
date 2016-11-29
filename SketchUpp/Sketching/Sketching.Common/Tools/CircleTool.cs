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
		IGeometryVisual ITool.Geometry {
			get {
				return Geometry;
			}

			set {
				throw new NotImplementedException();
			}
		}

		public CircleTool()
		{
			Name = "Circle";
		}
		public ToolType ToolType { get { return ToolType.Circle; } }

		private void Init() 
		{
			Geometry = new Circle(Geometry);
		}

		public void TouchStart(Point p)
		{
			Geometry.Start = p;
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Init();
		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}
	}
}
