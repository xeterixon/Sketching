using Android.App;
using Android.Content.PM;
using Sketching.Common.Pages;
using Sketching.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly : ExportRenderer(typeof(FixedRotationPage),typeof(FixedRotationPageRenderer))]
namespace Sketching.Droid
{
	public class FixedRotationPageRenderer : PageRenderer
	{
		private ScreenOrientation _previousOrientation = ScreenOrientation.Unspecified;
		protected override void OnWindowVisibilityChanged(Android.Views.ViewStates visibility)
		{
			base.OnWindowVisibilityChanged(visibility);
			var activity = (Activity)Context;
			if (visibility == Android.Views.ViewStates.Gone) 
			{
				activity.RequestedOrientation = _previousOrientation;				
			}
			else if (visibility == Android.Views.ViewStates.Visible) {
				if (_previousOrientation == ScreenOrientation.Unspecified) {
					_previousOrientation = activity.RequestedOrientation;
				}
				var page = (FixedRotationPage)Element;
				if (page.Orientation != PageOrientation.Default) 
				{
					activity.RequestedOrientation = page.Orientation == PageOrientation.Landscape ? ScreenOrientation.SensorLandscape : ScreenOrientation.SensorPortrait;
				}
			}
		}
	}
}

