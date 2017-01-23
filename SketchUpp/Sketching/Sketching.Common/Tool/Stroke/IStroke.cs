using System.Collections.Generic;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Stroke
{
	public interface IStroke : IGeometryVisual
	{
		List<Point> Points { get; set; }
		bool HighLight { get; set; }
	}
	public interface ILine : IStroke { }
}
