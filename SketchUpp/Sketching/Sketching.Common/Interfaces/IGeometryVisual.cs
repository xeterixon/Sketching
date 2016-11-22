using System;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IGeometryVisual
	{
		double Size { get; set; }
		Color Color { get; set; }
		bool IsValid { get; }
	}
}
