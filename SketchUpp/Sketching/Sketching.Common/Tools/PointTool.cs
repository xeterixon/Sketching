using Xamarin.Forms;
namespace Sketching.Common.Tools
{
	public class PointTool : PointToolBase
	{
		public PointTool()
		{
			Name = "Point";
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
			Init();
		}
	}
}
