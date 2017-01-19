
using Sketching.Interfaces;

namespace Sketching.Tool.Circle
{
	public interface ICircle : IGeometryVisual, ITwoPoint 
	{
		double Radius { get; }
	}
}
