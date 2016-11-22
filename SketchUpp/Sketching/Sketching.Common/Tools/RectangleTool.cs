using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class RectangleTool : ITool
	{
		public RectangleTool()
		{
			Name = "Rectangle";
			ToolType = ToolType.Rectangle;
		}

		public bool Active { get; set; }

		public IGeometryVisual Geometry { get; set; } = new Geometries.Rectangle(Xamarin.Forms.Color.Red, 10);

		public string Name { get; set; } 
		private IRectangle Rectangle {get {return (IRectangle)Geometry;}}
		public ToolType ToolType { get; set; }

		public void Activate()
		{
			Active = true;
		}

		public void Deactivate()
		{
			Active = false;
		}

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
