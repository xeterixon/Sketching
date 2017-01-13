using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public partial class TextInputView : ContentView, ITextInput
	{
		public event EventHandler<string> TextEntered;
		public string Text { get; set; }
		~TextInputView() 
		{
			System.Diagnostics.Debug.WriteLine("TextInput going down");
		}
		public INavigation NavigationProxy { get; set; }
		public View View{get{return this;}}
		private bool canPop = true; 
		public TextInputView()
		{
			InitializeComponent();

			BindingContext = this;
		}

		private void Entry_OnCompleted(object sender, EventArgs e)
		{
			if (!canPop) return;
			canPop = false;
			if (string.IsNullOrEmpty(Text))
				return;

			TextEntered?.Invoke(this, Text);
			//Navigation.PopAsync();
		}


		protected override async void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == WidthProperty.PropertyName)
			{
				if (Device.OS == TargetPlatform.Windows)
					await Task.Delay(TimeSpan.FromMilliseconds(200));

				textInput.Focus();
			}
		}

		public async Task Begin()
		{
			await NavigationProxy.PushAsync(new ContentPage { Content = this });
		}

		public async Task End()
		{
			await NavigationProxy.PopAsync();
		}
	}
}
