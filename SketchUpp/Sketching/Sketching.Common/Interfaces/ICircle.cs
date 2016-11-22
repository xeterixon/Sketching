using System;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface ICircle : ITwoPoint, IGeometryVisual
	{
		double Radius { get; }
	}
}
