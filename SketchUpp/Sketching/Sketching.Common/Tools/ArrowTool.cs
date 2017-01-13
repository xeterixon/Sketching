using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Helper;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class ArrowTool : IArrowTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IArrow Geometry { get; set; } = new Arrow();
		public bool CanUseFill { get; set; } = false;

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

		public ArrowTool() : this(ToolNames.ArrowTool, 1, 20, 8, null) { }

		public ArrowTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomColors = customColors;
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Geometry = new Arrow(Geometry);
		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}

		public void TouchStart(Point p)
		{
			Geometry.Start = p;
		}

		public IEnumerable<Color> CustomColors { get; set; }
	}
}
