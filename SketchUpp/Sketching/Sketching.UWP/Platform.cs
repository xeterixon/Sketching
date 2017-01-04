using Sketching.UWP;
using Sketching.UWP.Helper;

// ReSharper disable once CheckNamespace
namespace Sketching
{
	public static class Platform
	{
		public static void Init()
		{
			Sketching.Common.Helper.Image.ImageMetaDataImplementation = new ImageMetaData();
			var ignore1 = new SketchAreaRenderer();
			var ignore2 = new FixedRotationPageRenderer();
		}
	}
}
