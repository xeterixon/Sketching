﻿using System.Collections.Generic;
using Sketching.Common.Helper;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class PointTool : PointToolBase
	{
		/// <summary>
		/// PointTool with default values
		/// </summary>
		public PointTool() : this(ToolNames.PointTool, 10, 70, 40, string.Empty, null) { }

		/// <summary>
		/// Customized PointTool with default sizes
		/// </summary>
		public PointTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 10, 70, 40, customToolbarName, customToolbarColors) { }

		/// <summary>
		/// Custom made PointTool
		/// </summary>
		public PointTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors)
		{
			CanUseFill = false;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomToolbarName = customToolbarName;
			CustomToolbarColors = customToolbarColors;
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
			CreateNewGeometry();
		}
	}
}
