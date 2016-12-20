using System;
namespace Sketching.Common.Interfaces
{
	public interface IGeometryRenderer 
	{
		void Render(SkiaSharp.SKCanvas canvas, IGeometryVisual geometry);
		Type GeometryType { get; }
	}
}
