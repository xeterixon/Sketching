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

		public TextInputView()
		{
			InitializeComponent();

			BindingContext = this;
		}

		private void Entry_OnCompleted(object sender, EventArgs e)
		{
			TextEntryCompleted?.Invoke(this, Text);

			Navigation.PopAsync();
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			if (propertyName == WidthProperty.PropertyName)
			{
				textInput.Focus();
			}
			base.OnPropertyChanged(propertyName);
		}
	}
}
