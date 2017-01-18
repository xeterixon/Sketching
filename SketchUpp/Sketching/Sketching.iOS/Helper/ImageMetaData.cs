using System;
using System.Threading.Tasks;
using Foundation;
using Sketching.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace Sketching.iOS.Helper
{
	public class ImageMetaData : IImageMetaData
	{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task<Size> ImageSize(byte[] imageData)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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
