using System;
namespace Sketching.Interfaces
{
	public interface IGeometryRenderer 
	{
		void Render(SkiaSharp.SKCanvas canvas, IGeometryVisual geometry, double scale = 1.0);
		Type GeometryType { get; }
	}
}
