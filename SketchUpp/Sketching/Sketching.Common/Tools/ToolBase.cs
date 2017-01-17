﻿using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{

	public abstract class StrokeToolBase : IStrokeTool
	{
		protected virtual void CreateNewGeometry()
		{
			Geometry = new Stroke(Geometry);
		}
		public bool Active { get; set; }
		public virtual void TouchStart(Point p) { }
		public virtual void TouchMove(Point p) { }
		public virtual void TouchEnd(Point p) { }
		public string Name { get; set; }
		public IStroke Geometry { get; set; } = new Stroke();
		public bool CanUseFill { get; set; } = true;

		IGeometryVisual ITool.Geometry
		{
			get
			{
				return Geometry;
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
	}

	public abstract class PointToolBase : IPointTool
	{
		protected virtual void CreateNewGeometry()
		{
			Geometry = new Mark(Geometry);
		}
		public bool Active { get; set; }
		public virtual void TouchStart(Point p) { }
		public virtual void TouchMove(Point p) { }
		public virtual void TouchEnd(Point p) { }
		public string Name { get; set; }
		public IMark Geometry { get; set; } = new Mark();
		public bool CanUseFill { get; set; } = false;

		IGeometryVisual ITool.Geometry
		{
			get
			{
				return Geometry;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
	}
}
