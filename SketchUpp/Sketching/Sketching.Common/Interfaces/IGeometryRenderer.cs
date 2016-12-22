using System;
namespace Sketching.Common.Interfaces
{
	public interface IGeometryRenderer 
	{
		void Render(SkiaSharp.SKCanvas canvas, IGeometryVisual geometry, double scale = 1.0);
		Type GeometryType { get; }
	}
}
