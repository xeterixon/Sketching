using Sketching.Common.Helper;
using Sketching.Droid;
using Sketching.Droid.Helper;

// ReSharper disable once CheckNamespace
namespace Sketching
{
	public static class Platform
	{
		public static void Init() 
		{
			Image.ImageMetaDataImplementation = new ImageMetaData();
			var ignore1 = new SketchViewRenderer();
			var ignore2 = new FixedRotationPageRenderer();

		}
	}
}
