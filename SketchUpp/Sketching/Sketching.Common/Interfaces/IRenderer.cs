using SkiaSharp;

namespace Sketching.Common.Interfaces
{
	public interface IRenderer
	{
		void Setup(SKCanvas canvas);
		void Render(SKCanvas canvas);
	}
}
