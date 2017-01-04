using System;
using Foundation;
using Sketching.Common.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace Sketching.iOS.Helper
{
	public class ImageMetaData : IImageMetaData
	{
		public Size ImageSize(byte[] imageData)
		{
			try {
				
				var image = new UIImage(NSData.FromArray(imageData));
				var size =  new Size(image.Size.Width, image.Size.Height);
				image.Dispose();
				return size;
			} catch (Exception e) {
				Console.WriteLine("Failed to load imageData " + e.Message);
			}
			return Size.Zero;
		}
	}
}
