using SkiaSharp;

namespace Sketching.Common.Interfaces
{
	public interface IRenderer
	{
		void Setup(SKCanvas canvas, double scale = 1.0);
		void Render(SKCanvas canvas, double scale=1.0);
	}
}
