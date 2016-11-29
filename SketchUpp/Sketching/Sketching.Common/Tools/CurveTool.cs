using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		public CurveTool()
		{
			Name = "Curve";
			Geometry = new Geometries.Stroke { Color = new Color(1,0,0,0.5) , Size=10};
		}
		public override ToolType ToolType { get { return ToolType.Curve; } }

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
			Geometry.Points.Add(p);
		}
	}
}
