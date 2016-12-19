using System.Collections.Generic;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface ICustomColorSetup
	{
		IEnumerable<Color> CustomColors { get; set; }
	}
}
