using Sketching.Interfaces;

namespace Sketching.Tool.Text
{
	public interface IText : IGeometryVisual, IPoint
	{
		string Value { get; set; }
	}
}
