using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface ITouchDelegate
	{
		void TouchStart(Point p);
		void TouchEnd(Point p);
		void TouchMove(Point p);
	}
}
