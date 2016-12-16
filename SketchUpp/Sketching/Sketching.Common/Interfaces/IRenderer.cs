using System;
namespace Sketching.Common.Interfaces
{
	public interface IRenderer 
	{
		void Render(SkiaSharp.SKCanvas canvas, IGeometryVisual geometry);
		Type GeometryType { get; }
	}
}
