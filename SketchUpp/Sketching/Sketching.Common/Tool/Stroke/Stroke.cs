using System.Collections.Generic;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Stroke
{
	public class Stroke : IStroke
	{
		public Stroke() : this(new ToolSettings { SelectedColor = Color.Black }, 8, false) { }
		public Stroke(IGeometryVisual src) : this(src.ToolSettings, src.Size, src.IsFilled) { }
		public Stroke(ToolSettings toolSettings, double size, bool isFilled)
		{
			ToolSettings = toolSettings;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public double Size { get; set; }
		public ToolSettings ToolSettings { get; set; }
		public bool IsFilled { get; set; }
		public List<Point> Points { get; set; } = new List<Point>();
		public bool HighLight { get; set; } = false;
		public bool IsValid => Points.Count > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
	}
}
