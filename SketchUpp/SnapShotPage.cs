using System;
using System.IO;
using Sketching.Interfaces;
using Sketching.Pages;
using Xamarin.Forms;

namespace SketchUpp
{
	public class SnapShotPage : FixedRotationPage
	{
		public void SetImage(IImage image)
		{
			Orientation = image.Width > image.Height ? PageOrientation.Landscape : PageOrientation.Portrait;
			Title = "Image";
			var i = new Image();
			i.Source = ImageSource.FromStream(() => new MemoryStream(image.Data));
			Content = i;
		}
	}
}

