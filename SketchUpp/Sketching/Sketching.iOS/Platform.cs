
using Sketching.iOS;
using Sketching.iOS.Helper;

// ReSharper disable once CheckNamespace
namespace Sketching
{
	public static class Platform
	{
		public static void Init()
		{
			var ignore1 = new SketchViewRenderer();
			Sketching.Common.Helper.Image.ImageMetaDataImplementation = new ImageMetaData();
		}
	}
}
