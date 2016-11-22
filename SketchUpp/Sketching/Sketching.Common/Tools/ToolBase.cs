using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{

	public abstract class StrokeToolBase : IStrokeTool
	{
		public IGeometryVisual Geometry { get; set; } = new Stroke(Xamarin.Forms.Color.Blue, 10);
		protected virtual void Init()
		{
			Geometry = new Stroke(Stroke);
		}
		protected IStroke Stroke 
		{
			get { return (IStroke)Geometry; }
		}
		public bool Active { get; set; }
		public virtual void Activate()
		{
			Active = true;
			Init();
		}
		public virtual void Deactivate()
		{
			Active = false;
		}
		public virtual void TouchStart(Point p){ }
		public virtual void TouchMove(Point p) {}
		public virtual void TouchEnd(Point p) {}

		public string Name { get; set; }
		public ToolType ToolType { get; set; }
	}
	public abstract class PointToolBase : IPointTool 
	{
		protected virtual void Init()
		{
			Geometry = new Mark(Mark);
		}
		public bool Active { get; set; }
		protected IMark Mark 
		{
			get { return (IMark)Geometry;}
		}
		public virtual void Activate()
		{
			Active = true;
			Init();
		}
		public virtual void Deactivate()
		{
			Active = false;
		}
		public virtual void TouchStart(Point p) {}
		public virtual void TouchMove(Point p) { }
		public virtual void TouchEnd(Point p) { }

		public string Name { get; set; }
		public ToolType ToolType { get; set; }
		public IGeometryVisual Geometry { get; set; } = new Mark();

	}
}
