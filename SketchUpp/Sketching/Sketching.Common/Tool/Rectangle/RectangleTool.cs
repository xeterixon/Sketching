using System;
using System.Collections.Generic;
using Sketching.Helper;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Rectangle
{
	public class RectangleTool : IRectangleTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IRectangle Geometry { get; set; } = new Rectangle();
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

		/// <summary>
		/// RectangleTool with default values
		/// </summary>
		public RectangleTool() : this(ToolNames.RectangleTool, 1, 20, 8, string.Empty, null) { }

		/// <summary>
		/// Customized RectangleTool with default sizes
		/// </summary>
		public RectangleTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 1, 20, 8, customToolbarName, customToolbarColors) { }

		/// <summary>
		/// Custom made RectangleTool
		/// </summary>
		public RectangleTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors)
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
			Geometry = new Rectangle(Geometry);
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
	}
}
