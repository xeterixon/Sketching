using System;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface ITwoPoint
	{
		Point Start { get; set; }
		Point End { get; set; }
	}
}
