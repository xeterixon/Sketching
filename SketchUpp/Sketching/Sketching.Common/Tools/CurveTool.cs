using System.Collections.Generic;
using Sketching.Common.Helper;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		public CurveTool() : this(ToolNames.CurveTool, 1, 20, 8, null) { }

		public CurveTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			CanUseFill = true;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.HighLight = false;
			CustomColors = customColors;
		}

		public override void TouchStart(Point p)
		{
			base.TouchStart(p);
			AddPoint(p);
		}

		public override void TouchMove(Point p)
		{
			base.TouchMove(p);
			AddPoint(p);
		}

		public override void TouchEnd(Point p)
		{
			base.TouchEnd(p);
			AddPoint(p);
			CreateNewGeometry();
		}

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}
