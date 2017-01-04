using System;
using System.IO;
using Sketching.Common.Interfaces;
using Sketching.Common.Pages;
using Xamarin.Forms;

namespace SketchUpp
{
	public class SnapShotPage : FixedRotationPage
	{
		public SnapShotPage(IImage image)
		{
			Orientation = image.Width > image.Height ? PageOrientation.Landscape : PageOrientation.Portrait;
			Title = "Image";
			var i = new Image();
			i.Source = ImageSource.FromStream(() => new MemoryStream(image.Data));
			var size = Sketching.Common.Helper.Image.ImageSize(image.Data);
			Content = i;
			
		}
	}
}

