using System;
using System.Threading.Tasks;
using Android.Graphics;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Droid.Helper
{
	public class ImageMetaData : IImageMetaData
	{
		public async Task<Size> ImageSize(byte[] imageData)
		{
			try {
				var options = new BitmapFactory.Options {
					InJustDecodeBounds = true
				};
				var image = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length, options);
				var size = new Size(options.OutWidth, options.OutHeight);
				image?.Dispose();
				return size;
			} catch (Exception e) {
				Console.WriteLine("Failed to load imageData " + e.Message);
			}
			return Size.Zero;
		}
	}
	
}
