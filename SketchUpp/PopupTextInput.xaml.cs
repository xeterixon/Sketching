using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace SketchUpp
{
	public class InputProxy : ITextInput
	{
		public INavigation NavigationProxy { get; set; }

		public event EventHandler<string> TextEntered;
		public void SetText(string text) 
		{
			TextEntered?.Invoke(this, text);	
		}
		public async Task Begin()
		{
			await NavigationProxy.PushPopupAsync(new PopupTextInput(this));
		}

		public async Task End()
		{
			await NavigationProxy.PopPopupAsync();
		}
	}

	public partial class PopupTextInput : PopupPage
	{

		InputProxy _proxy;
		public PopupTextInput(InputProxy proxy)
		{
			_proxy = proxy;
			InitializeComponent();
		}

		void Handle_Completed(object sender, System.EventArgs e)
		{
			_proxy.SetText(InputView.Text);
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await _proxy.End();
		}

		// Method for animation child in PopupPage
		// Invoced after custom animation end
		protected override Task OnAppearingAnimationEnd()
		{
			InputView.Focus();
			return Task.FromResult(0);

		}

		protected override bool OnBackButtonPressed()
		{
			// Prevent hide popup
			//return base.OnBackButtonPressed();
			return true;
		}

		// Invoced when background is clicked
		protected override bool OnBackgroundClicked()
		{
			return base.OnBackgroundClicked();
		}
	}
}
