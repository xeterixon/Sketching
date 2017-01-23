using Xamarin.Forms;

namespace Sketching.Views
{
	public class ToolPaletteItem : Label
	{
		public Color ItemColor
		{
			get { return BackgroundColor; }
			set { BackgroundColor = value; }
		}

		public string ItemText
		{
			get { return Text; }
			set { Text = value; }
		}
	}
}
