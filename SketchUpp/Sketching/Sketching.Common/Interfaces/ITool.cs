using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public enum ToolType
	{
		Undefined,
		Line,
		Point,
		Curve,
		MultiLine,
		Rectangle,
		Circle,
		Text
	}
	public interface ITool<T> : ITool 
		where T: IGeometryVisual
	{
		new T Geometry { get; set; }
	}
	public interface ITool : ITouchDelegate
	{
		string Name { get; set; }
		ToolType ToolType { get; } //TODO Remove?
		bool Active { get; set; }
		IGeometryVisual Geometry{get;set;}
	}

	public interface IStrokeTool : ITool<IStroke>{}
	public interface ILineTool : ITool<ILine> {}
	public interface ICircleTool : ITool<ICircle> {}
	public interface IPointTool : ITool<IMark> {}

}
