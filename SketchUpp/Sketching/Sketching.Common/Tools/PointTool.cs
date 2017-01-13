using System.Collections.Generic;
using Sketching.Common.Helper;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class PointTool : PointToolBase
	{
		public PointTool() : this(ToolNames.PointTool, 10, 70, 40, null) { }

		public PointTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			CanUseFill = false;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomColors = customColors;
		}

		public override void TouchStart(Point p)
		{
			base.TouchStart(p);
			Geometry.Point = p;
		}

		public override void TouchMove(Point p)
		{
			base.TouchMove(p);
			Geometry.Point = p;
		}

		public override void TouchEnd(Point p)
		{
			base.TouchEnd(p);
			CreateNewGeometry();
		}
	}
}
