namespace Sketching.Common.Interfaces
{
	public interface IText : IGeometryVisual, IPoint
	{
		string Value { get; set; }
	}
}
