using Sketching.iOS;
using Sketching.iOS.Helper;

// ReSharper disable once CheckNamespace
namespace Sketching
{
	public static class Platform
	{
		public static void Init()
		{
			Bootstrap.Init();
#pragma warning disable 219
			var ignore1 = new SketchViewRenderer();
			Helper.Image.ImageMetaDataImplementation = new ImageMetaData();
#pragma warning restore 219
		}
	}
}
