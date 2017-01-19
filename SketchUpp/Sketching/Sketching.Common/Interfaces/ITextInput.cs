using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sketching.Interfaces
{
	public interface ITextInput
	{
		INavigation NavigationProxy { get; set; }
		Task Begin(); // Shows some kind of input control
		event EventHandler<string> TextEntered; 
		Task End(); // Should be called from consuming object when done
	}
}
