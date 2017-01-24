using System.Collections.Generic;
using Sketching.Helper;
using Xamarin.Forms;

namespace Sketching.Tool.Stroke
{
	public class HighlightTool : StrokeToolBase
	{
		/// <summary>
		/// HighlightTool with default values
		/// </summary>
		public HighlightTool() : this(ToolNames.HighlightTool, 1, 100, 50, string.Empty, null) { }

		/// <summary>
		/// Customized HighlightTool with default sizes
		/// </summary>
		public HighlightTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 1, 100, 50, customToolbarName, customToolbarColors) { }

		/// <summary>
		/// Custom made HighlightTool
		/// </summary>
		public HighlightTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors)
		{
			CanUseFill = false;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.HighLight = true;
			Geometry.ToolSettings.SelectedColor = Color.Yellow;
			CustomToolbarName = customToolbarName;
			CustomToolbarColors = customToolbarColors;
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
			CreateNewGeometry();
		}

		protected override void CreateNewGeometry()
		{
			Geometry = new Stroke
			{
				MinSize = Geometry.MinSize,
				MaxSize = Geometry.MaxSize,
				Size = Geometry.Size,
				HighLight = Geometry.HighLight,
				ToolSettings = Geometry.ToolSettings
			};
		}

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}