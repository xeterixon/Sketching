// ReSharper disable once CheckNamespace
using Sketching.iOS.Helper;

namespace Sketching
{
	public static class Platform
	{
		public static void Init()
		{
			var ignore = new Sketching.iOS.SketchViewRenderer();
			Sketching.Common.Helper.Image.ImageMetaDataImplementation = new ImageMetaData();
		}
	}
}
