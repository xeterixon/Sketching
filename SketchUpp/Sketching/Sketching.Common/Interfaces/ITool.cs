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
		Circle
	}

	public interface ITool : ITouchDelegate
	{
		string Name { get; set; }
		ToolType ToolType { get; set; }
		void Activate();
		void Deactivate();
		bool Active { get; set; }
		IGeometryVisual Geometry{get;set;}
	}
	public interface IStrokeTool : ITool
	{
	}
	public interface ICircleTool : ITool 
	{
	}
	public interface IPointTool : ITool 
	{
	}

}
