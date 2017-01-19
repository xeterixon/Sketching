using System.Threading.Tasks;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Helper
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
