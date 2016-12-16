using Sketching.Common.Geometries;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		public CurveTool() : this("Curve", 8, 20, 1){}

		public CurveTool(string name, double size, double maxSize, double alpha)
		{
			Name = name;
			Geometry = new Stroke
			{
				MaxSize = maxSize,
				Size = size
			};
			Geometry.Color = Geometry.Color.MultiplyAlpha(alpha);
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
				Color = Geometry.Color
			};
		}

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}
