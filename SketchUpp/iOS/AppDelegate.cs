using Foundation;
using Sketching.Common.Pages;
using UIKit;
using Xamarin.Forms;

namespace SketchUpp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			Sketching.Platform.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
		private void HandleOrientation(Page p, ref UIInterfaceOrientationMask mask) 
		{
			if (p is NavigationPage) 
			{
				HandleOrientation(((NavigationPage)p).CurrentPage,ref mask);
			}
			if (p is FixedRotationPage) 
			{
				var o = ((FixedRotationPage)p).Orientation;
				switch (o) {
				case PageOrientation.Landscape:
					mask = UIInterfaceOrientationMask.Landscape;
					break;
				case PageOrientation.Portrait:
					mask = UIInterfaceOrientationMask.Portrait | UIInterfaceOrientationMask.PortraitUpsideDown;
					break;

				}
			}
		}
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
		{
			var prevMask = forWindow?.RootViewController?.GetSupportedInterfaceOrientations();
			UIInterfaceOrientationMask mask = prevMask?? UIInterfaceOrientationMask.All; 

			if (Xamarin.Forms.Application.Current != null && Xamarin.Forms.Application.Current.MainPage != null) 
			{
				var main = Xamarin.Forms.Application.Current.MainPage;
				HandleOrientation(main, ref mask);
			}
			return mask;
		}
	}
}
