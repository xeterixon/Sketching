using System.Collections.Generic;
using Sketching.Common.Geometries;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class HighlightTool : StrokeToolBase
	{
		public HighlightTool() : this("Highlight", 50, 100, null) { }

		public HighlightTool(string name, double size, double maxSize, IEnumerable<Color> customColors)
		{
			Name = name;
			CanUseFill = false;
			Geometry = new Stroke
			{
				MaxSize = maxSize,
				Size = size,
				HighLight = true
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
			Init();
		}

		protected override void Init()
		{
			Geometry = new Stroke
			{
				Size = Geometry.Size,
				MaxSize = Geometry.MaxSize,
				Color = Geometry.Color,
				HighLight = Geometry.HighLight
			};
		}

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}
