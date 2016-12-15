using System;
using Sketching.Common.Interfaces;

namespace Sketching.Common.Tools
{
	public class PointTool : PointToolBase
	{
		public PointTool()
		{
			Name = "Point";
		}
		public override void TouchStart(Xamarin.Forms.Point p)
		{
			base.TouchStart(p);
			Geometry.Point = p;
		}
		public override void TouchMove(Xamarin.Forms.Point p)
		{
			base.TouchMove(p);
			Geometry.Point = p;
		}
		public override void TouchEnd(Xamarin.Forms.Point p)
		{
			base.TouchEnd(p);
			Init();
		}
	}
}
