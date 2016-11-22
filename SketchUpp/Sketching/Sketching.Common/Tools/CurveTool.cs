using System;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		public CurveTool()
		{
			Name = "Curve";
			ToolType = Interfaces.ToolType.Line;
			Geometry = new Geometries.Stroke { Color = new Color(1,0,0,0.5) , Size=10};

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
		private void AddPoint(Point p)
		{
			Stroke.Points.Add(p);
		}
	}
}
