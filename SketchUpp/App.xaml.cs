using Sketching.Common.Render;
using SketchUpp.CustomTool;
using Xamarin.Forms;

namespace SketchUpp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Sketching.Common.Helper.Factory.RegisterTextInput(typeof(InputProxy));
			//GeometryRenderer.AddRenderer(new OvalRenderer());
			var sketchPage = new SketchPage();
			MainPage = new NavigationPage(sketchPage);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
