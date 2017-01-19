using Sketching;
using Sketching.Helper;
using Sketching.Droid;
using Sketching.Droid.Helper;

// ReSharper disable once CheckNamespace
namespace Sketching
{
	public static class Platform
	{
		public static void Init() 
		{
			Bootstrap.Init();
			Image.ImageMetaDataImplementation = new ImageMetaData();
#pragma warning disable 219

			var ignore1 = new SketchViewRenderer();
			var ignore2 = new FixedRotationPageRenderer();

#pragma warning restore 219


		}
	}
}
