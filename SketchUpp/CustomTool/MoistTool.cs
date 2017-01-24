using System;
using System.Collections.Generic;
using System.Linq;
using Sketching.Interfaces;
using Sketching.Tool;
using Xamarin.Forms;

namespace SketchUpp.CustomTool
{
	public class MoistTool : ITool<IMoist>
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IMoist Geometry { get; set; } = new Moist();
		public bool CanUseFill { get; set; }
		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
		public bool ShowDefaultToolbar { get; set; }

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
		/// PointTool with default values
		/// </summary>
		public MoistTool() : this("Moist", 20, 120, 60, string.Empty, null) { }

		/// <summary>
		/// Customized PointTool with default sizes
		/// </summary>
		public MoistTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 20, 120, 60, customToolbarName, customToolbarColors.ToList()) { }

		/// <summary>
		/// Custom made PointTool
		/// </summary>
		public MoistTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, List<KeyValuePair<string, Color>> customToolbarColors)
		{
			CanUseFill = false;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.SelectedItem.ItemColor = customToolbarColors != null && customToolbarColors.Any() ? customToolbarColors.First().Value : Color.Black;
			Geometry.SelectedItem.ItemText = customToolbarColors != null && customToolbarColors.Any() ? customToolbarColors.First().Key : "";
			CustomToolbarName = customToolbarName;
			CustomToolbarColors = customToolbarColors;
		}

		public void TouchEnd(Point p)
		{
			Geometry.Point = p;
			Geometry = new Moist(Geometry);
		}

		public void TouchMove(Point p)
		{
			Geometry.Point = p;
		}

		public void TouchStart(Point p)
		{
			Geometry.Point = p;
		}
	}
}
