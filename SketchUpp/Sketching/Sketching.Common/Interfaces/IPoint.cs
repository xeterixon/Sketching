using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IPoint 
	{
		Point Point { get; set; }
	}

	public interface ITwoPoint
	{
		Point Start { get; set; }
		Point End { get; set; }
	}
}
