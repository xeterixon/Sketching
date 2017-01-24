using Sketching.Renderer;
using SketchUpp.CustomTool;
using Xamarin.Forms;

namespace SketchUpp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Sketching.Helper.Factory.RegisterTextInput(typeof(InputProxy));
			Sketching.Renderer.GeometryRenderer.AddRenderer(new RulerTool.RulerRenderer());

			MainPage = new NavigationPage(new LandingPage());
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
