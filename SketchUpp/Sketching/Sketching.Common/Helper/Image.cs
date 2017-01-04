using System;
using System.Threading.Tasks;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Helper
{
	public static class Image
	{
		//This is set from platform-dependant code
		public static IImageMetaData ImageMetaDataImplementation;
		public static async Task<Size> ImageSize(byte [] imageData) 
		{
			return await ImageMetaDataImplementation.ImageSize(imageData);
		}
	}
}
