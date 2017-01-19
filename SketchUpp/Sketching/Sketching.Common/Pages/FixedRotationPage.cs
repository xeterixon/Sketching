using Xamarin.Forms;

namespace Sketching.Pages
{
	public enum PageOrientation 
	{
		Default,
		Portrait,
		Landscape,
	}
	public class FixedRotationPage : ContentPage
	{
		public FixedRotationPage()
		{
			Orientation = PageOrientation.Default;	
		}
		public PageOrientation Orientation { get; protected set;}

	}
}

