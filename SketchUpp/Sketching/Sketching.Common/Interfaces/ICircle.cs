using System;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface ICircle : IGeometryVisual, ITwoPoint 
	{
		double Radius { get; }
	}
}
