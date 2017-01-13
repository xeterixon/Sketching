using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Helper;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class HighlightTool : StrokeToolBase
	{
		public HighlightTool() : this(ToolNames.HighlightTool, 1, 100, 50, null) { }

		public HighlightTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			CanUseFill = false;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.HighLight = true;
			Geometry.Color = Color.Yellow;
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

		protected override void CreateNewGeometry()
		{
			Geometry = new Stroke
			{
				MinSize = Geometry.MinSize,
				MaxSize = Geometry.MaxSize,
				Size = Geometry.Size,
				HighLight = Geometry.HighLight,
				Color = Geometry.Color
			};
		}

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}
