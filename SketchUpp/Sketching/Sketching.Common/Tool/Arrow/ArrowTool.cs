using System;
using System.Collections.Generic;
using Sketching.Helper;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Arrow
{
	public class ArrowTool : IArrowTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IArrow Geometry { get; set; } = new Arrow();
		public bool CanUseFill { get; set; } = false;
		public bool CanUseStencil { get; set; } = false;

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

		/// <summary>
		/// ArrowTool with default values
		/// </summary>
		public ArrowTool() : this(ToolNames.ArrowTool, 1, 20, 8, string.Empty, null) { }

		/// <summary>
		/// Customized ArrowTool with default sizes
		/// </summary>
		public ArrowTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 1, 20, 8, customToolbarName, customToolbarColors) { }

		/// <summary>
		/// Custom made ArrowTool
		/// </summary>
		public ArrowTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors)
		{
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomToolbarName = customToolbarName;
			CustomToolbarColors = customToolbarColors;
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

		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
		public bool ShowDefaultToolbar { get; set; } = true;
	}
}
