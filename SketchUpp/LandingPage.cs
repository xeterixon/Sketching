using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SketchUpp
{
	public class LandingPage : ContentPage
	{
		public LandingPage()
		{
			Content = new Button
			{
				Text = "Start drawing" ,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Command = new Command(async ()=> { await PushSketchPage(); })
			};
			Title = "Welcome";
		}

		private async Task PushSketchPage()
		{
			await Navigation.PushAsync(new SketchPage());
		}
	}
}
