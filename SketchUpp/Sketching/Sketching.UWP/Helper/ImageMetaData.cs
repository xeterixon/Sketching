using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.UWP.Helper
{
	public class ImageMetaData : IImageMetaData
	{
		public async Task<Size> ImageSize(byte[] imageData)
		{
			try
			{
				var memStream = new MemoryStream(imageData);
				IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
				var decoder =  await BitmapDecoder.CreateAsync(imageStream).AsTask().ConfigureAwait(false);
				imageStream.Dispose();
				var size = new Size(decoder.PixelWidth,decoder.PixelHeight);
				return size;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return Size.Zero;
		}
	}
}
