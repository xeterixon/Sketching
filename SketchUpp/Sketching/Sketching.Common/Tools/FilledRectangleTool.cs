using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class FilledRectangleTool : ITool
	{
		public FilledRectangleTool()
		{
			Name = "FilledRectangle";
		}

		public bool Active { get; set; }
		public IGeometryVisual Geometry { get; set; } = new Geometries.FilledRectangle();
		public bool CanUseFill { get; set; } = true;
		public string Name { get; set; }
		private IFilledRectangle FilledRectangle => (IFilledRectangle)Geometry;

		public void TouchEnd(Point p)
		{
			FilledRectangle.End = p;
			Geometry = new Geometries.FilledRectangle(FilledRectangle);
		}

		public void TouchMove(Point p)
		{
			FilledRectangle.End = p;
		}

		public void TouchStart(Point p)
		{
			FilledRectangle.Start = p;
		}
	}
}
