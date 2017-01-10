using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IFilledRectangle : IGeometryVisual, ITwoPoint
	{
		Color FillColor { get; set; }
	}
}
