using System.Collections.Generic;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface ICustomColorSetup
	{
		string CustomToolbarName { get; set; }
		IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
	}
}
