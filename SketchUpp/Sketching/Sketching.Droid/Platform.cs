// ReSharper disable once CheckNamespace
using Sketching.Common.Helper;
using Sketching.Droid.Helper;

namespace Sketching
{
	public static class Platform
	{
		public static void Init() 
		{
			Image.ImageMetaDataImplementation = new ImageMetaData();
		}
	}
}
