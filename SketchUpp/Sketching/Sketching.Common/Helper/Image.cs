using System;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Helper
{
	public static class Image
	{
		//This is set from platform-dependant code
		public static IImageMetaData ImageMetaDataImplementation;
		public static Size ImageSize(byte [] imageData) 
		{
			return ImageMetaDataImplementation.ImageSize(imageData);
		}
	}
}
