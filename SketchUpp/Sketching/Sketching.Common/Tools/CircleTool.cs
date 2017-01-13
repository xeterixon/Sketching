using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Helper;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CircleTool : ICircleTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public ICircle Geometry { get; set; } = new Circle();
		public bool CanUseFill { get; set; } = true;

		IGeometryVisual ITool.Geometry
		{
			get
			{
				return Geometry;
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public CircleTool() : this(ToolNames.CircleTool, 1, 20, 8, null) { }

		public CircleTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomColors = customColors;
		}

		public void TouchStart(Point p)
		{
			Geometry.Start = p;
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			ReserGeometry();
		}

		private void ReserGeometry()
		{
			Geometry = new Circle(Geometry);
		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}

		public IEnumerable<Color> CustomColors { get; set; }
	}
}
