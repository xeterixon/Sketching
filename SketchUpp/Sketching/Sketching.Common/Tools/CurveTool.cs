using System.Collections.Generic;
using Sketching.Common.Geometries;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		public CurveTool() : this("Curve", 8, 20, null) { }

		public CurveTool(string name, double size, double maxSize, IEnumerable<Color> customColors)
		{
			CanUseFill = true;
			Name = name;
			CanUseFill = true;
			Geometry = new Stroke
			{
				MaxSize = maxSize,
				Size = size,
				HighLight = false
			};
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
