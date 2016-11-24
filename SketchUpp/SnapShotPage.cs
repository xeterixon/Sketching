using System;
using System.IO;
using Xamarin.Forms;

namespace SketchUpp
{
	public class SnapShotPage : ContentPage
	{
		public SnapShotPage(byte [] imageData)
		{
			Title = "Image";
			var image = new Image();
			image.Source = ImageSource.FromStream(() => new MemoryStream(imageData));
			Content = image;
			
		}
	}
}

