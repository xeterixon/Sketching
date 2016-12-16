using System;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		public CurveTool() : this("Curve", false){}

		public CurveTool(string name, bool useAsHighlighter)
		{
			Name = name;
			Geometry = new Stroke {IsHighlighter = useAsHighlighter};
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
			Geometry = new Stroke(Geometry) {IsHighlighter = Geometry.IsHighlighter};
		}

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}
