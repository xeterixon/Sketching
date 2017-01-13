using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Helper;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class OvalTool : IOvalTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IOval Geometry { get; set; } = new Oval();
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

		public OvalTool() : this(ToolNames.OvalTool, 1, 20, 8, null) { }

		public OvalTool(string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
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
			Geometry = new Oval(Geometry);
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
