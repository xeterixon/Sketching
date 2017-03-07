using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Text
{
	public class Text : IText
	{
		public Text() : this(new ToolSettings { SelectedColor = Color.Black }, 75, false) { }
		public Text(IGeometryVisual src) : this() 
		{
			src.CopyTo(this);
		}
		public Text(ToolSettings toolSettings, double size, bool isFilled)
		{
			Value = string.Empty;
			ToolSettings = toolSettings;
			IsFilled = isFilled;
			Size = size;
			Point = new Point(-1, -1);
			MinSize = 20;
			MaxSize = 200;
		}

		public ToolSettings ToolSettings { get; set; }
		public bool IsFilled { get; set; }
		public bool IsStenciled { get; set; } = false;
		public bool IsValid => !string.IsNullOrEmpty(Value) && Point.X > 0;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Point { get; set; }
		public double Size { get; set; }
		public string Value { get; set; }
		public bool RoundedFill { get; set; }
	}
}
