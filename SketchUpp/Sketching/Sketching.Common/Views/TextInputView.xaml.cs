using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public partial class TextInputView : ContentView
	{
		public event EventHandler<string> TextEntryCompleted;
		public string Text { get; set; }

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

			TextEntryCompleted?.Invoke(this, Text);

			Navigation.PopAsync();
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
	}
}
