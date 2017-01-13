using System.Windows.Input;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public class ToolSettingsViewModel
	{
		private readonly INavigation _navigation;
		public ICommand ColorSelectedCommand { get; set; }
		public ITool Tool { get; set; }
		public ToolSettingsViewModel(ITool tool, INavigation navigation)
		{
			_navigation = navigation;
			Tool = tool;
			ColorSelectedCommand = new Command<Color>(color =>
			{
				Tool.Geometry.Color = color;
				_navigation.PopAsync();
			});
		}
	}
}
