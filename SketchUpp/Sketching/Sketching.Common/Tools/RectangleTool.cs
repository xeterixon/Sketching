using System;
using System.Collections.Generic;
using Sketching.Common.Helper;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class RectangleTool : IRectangleTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IRectangle Geometry { get; set; } = new Geometries.Rectangle();
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

		public RectangleTool() : this(ToolNames.RectangleTool, 1, 20, 8, null) { }

		public RectangleTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
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
			Geometry = new Geometries.Rectangle(Geometry);
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
