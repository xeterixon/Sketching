using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{

	public abstract class StrokeToolBase : IStrokeTool
	{
		protected virtual void Init()
		{
			Geometry = new Stroke(Geometry);
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

		public IStroke Geometry { get; set;} = new Stroke(Xamarin.Forms.Color.Blue, 10);

		IGeometryVisual ITool.Geometry {
			get {
				return Geometry;
			}

			set {
				throw new NotImplementedException();
			}
		}
	}
	public abstract class PointToolBase : IPointTool 
	{
		protected virtual void Init()
		{
			Geometry = new Mark(Geometry);
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
		public virtual void TouchStart(Point p) {}
		public virtual void TouchMove(Point p) { }
		public virtual void TouchEnd(Point p) { }

		public string Name { get; set; }
		public ToolType ToolType { get; set; }

		public IMark Geometry { get; set; } = new Mark();

		IGeometryVisual ITool.Geometry {
			get {
				return Geometry;
			}
			set {
				throw new NotImplementedException();
			}
		}
	}
}
