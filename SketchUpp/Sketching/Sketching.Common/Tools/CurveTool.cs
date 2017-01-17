using System.Collections.Generic;
using Sketching.Common.Helper;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class CurveTool : StrokeToolBase
	{
		/// <summary>
		/// CurveTool with default values
		/// </summary>
		public CurveTool() : this(ToolNames.CurveTool, 1, 20, 8, string.Empty, null) { }

		/// <summary>
		/// Customized CurveTool with default sizes
		/// </summary>
		public CurveTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 1, 20, 8, customToolbarName, customToolbarColors) { }

		/// <summary>
		/// Custom made CurveTool
		/// </summary>
		public CurveTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors)
		{
			CanUseFill = true;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.HighLight = false;
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

		private void AddPoint(Point p)
		{
			Geometry.Points.Add(p);
		}
	}
}
