using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class RectangleTool : ITool
	{
		public RectangleTool()
		{
			Name = "Rectangle";
		}

		public bool Active { get; set; }
		public IGeometryVisual Geometry { get; set; } = new Geometries.Rectangle();

		public string Name { get; set; } 
		private IRectangle Rectangle => (IRectangle)Geometry;

		public void TouchEnd(Point p)
		{
			Rectangle.End = p;
			Geometry = new Geometries.Rectangle(Rectangle);
		}

		public void TouchMove(Point p)
		{
			Rectangle.End = p;
		}

		public void TouchStart(Point p)
		{
			Rectangle.Start = p;
		}
	}
}
