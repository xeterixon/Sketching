using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class LineTool : StrokeToolBase
	{
		private int GridSize => Config.GridSize;
		public LineTool()
		{
			Name = "Line";
			ToolType = Interfaces.ToolType.Line;
			Geometry = new Geometries.Stroke { Color = Xamarin.Forms.Color.Blue, Size = 10};
		}
		private void Snap(ref Point p)
		{
			if (GridSize < 0) return;
			var x = (int)((p.X + GridSize / 2) / GridSize) * GridSize;
			var y = (int)((p.Y + GridSize / 2) / GridSize) * GridSize;
			p.X = x;
			p.Y = y;

		}
		public override void TouchStart(Point p)
		{
			base.TouchStart(p);
			Snap(ref p);
			AddPoint(p);
			
		}
		public override void TouchMove(Point p)
		{
			base.TouchMove(p);
			Snap(ref p);
			AddPoint(p);
			
		}
		public override void TouchEnd(Point p)
		{
			base.TouchEnd(p);
			Init();
		}
		private void AddPoint(Point p)
		{
			if (Stroke.Points.Count < 2) {
				Stroke.Points.Add(p);
			} else if (Stroke.Points.Count == 2) {
				Stroke.Points[1] = p;
			}
		}

	}
}
