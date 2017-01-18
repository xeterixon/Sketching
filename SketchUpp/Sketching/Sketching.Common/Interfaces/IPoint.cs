using Xamarin.Forms;

namespace Sketching.Interfaces
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
