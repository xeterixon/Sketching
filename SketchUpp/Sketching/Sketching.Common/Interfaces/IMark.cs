using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IMark : IGeometryVisual
	{
		Point Point { get; set; }
	}
}
