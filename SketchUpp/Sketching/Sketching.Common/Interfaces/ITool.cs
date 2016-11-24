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
		ToolType ToolType { get; set; } //TODO Remove?
		void Activate(); //TODO Remove and use Active property only?
		void Deactivate(); //TODO Remove and use Active property only?
		bool Active { get; set; }
		IGeometryVisual Geometry{get;set;}
	}
	public interface IStrokeTool : ITool<IStroke>
	{
	}
	public interface ICircleTool : ITool<ICircle> 
	{
	}
	public interface IPointTool : ITool<IMark> 
	{
	}

}
