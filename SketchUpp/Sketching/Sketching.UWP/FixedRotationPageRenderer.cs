using Windows.Graphics.Display;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Sketching.Common.Pages;
using Sketching.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
[assembly: ExportRenderer(typeof(FixedRotationPage), typeof(FixedRotationPageRenderer))]
namespace Sketching.UWP
{
	class FixedRotationPageRenderer : PageRenderer
	{
		DisplayOrientations _prevOrientation = DisplayOrientations.None;
		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				if (!IsDesktop)
				{
					_prevOrientation = DisplayInformation.AutoRotationPreferences;
					var page = (FixedRotationPage) Element;
					var orientation = page?.Orientation;
					if (orientation.HasValue && orientation.Value != PageOrientation.Default)
					{
						DisplayInformation.AutoRotationPreferences = orientation.Value == PageOrientation.Landscape
							? DisplayOrientations.Landscape
							: DisplayOrientations.Portrait;
					}
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			DisplayInformation.AutoRotationPreferences = _prevOrientation;
		}

		private bool IsDesktop => UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse &&
		                          AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";

	}
}
