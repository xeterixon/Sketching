using System;
using System.Collections.Generic;
using System.Linq;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public interface IToolCollection : ITouchDelegate
	{
		ISketchView View { get; set; }
		List<ITool> Tools { get; set; }
		void ActivateTool(ITool tool);
		void ActivateTool(string toolName);
		List<IGeometryVisual> Geometries { get; set; }
		ITool ActiveTool { get; }
		void Undo();

	}
	public class ToolCollection : IToolCollection
	{
		public ITool ActiveTool {
			get 
			{
				return Tools.FirstOrDefault((arg) => arg.Active);
			}
		}
		public ISketchView View { get; set; }
		public List<ITool> Tools { get; set; } = new List<ITool>();
		public List<IGeometryVisual> Geometries { get; set; } = new List<IGeometryVisual>();
		//NOTE Only allow one active tool at a time for now.
		public void ActivateTool(ITool tool) 
		{
			if (tool == null) {
				throw new NullReferenceException("Tool is null");
			};
			var activeTools = Tools.Where(t => t.Active);
			foreach (var activeTool in activeTools) 
			{
				activeTool.Deactivate();
			}
			var tl = Tools.FirstOrDefault(t => object.ReferenceEquals(t, tool));
			if (tl == null) 
			{
				throw new InvalidOperationException("Don't activate a tool that's not registered");
			}
			tl.Activate();
		}
		public void ActivateTool(string toolName) 
		{
			ActivateTool(Tools.FirstOrDefault(t => t.Name == toolName));
		}
		public void TouchStart(Point p)
		{
			var activeTools = Tools.Where(t => t.Active);
			foreach (var tool in activeTools) 
			{
				tool.TouchStart(p);
				Geometries.Add(tool.Geometry);
			}
		}
		public void Undo()
		{
			var last = Geometries.LastOrDefault();
			if (last == null) return;
			Geometries.Remove(last);
			View?.CallbackToNative?.Invoke(CallbackType.Repaint);
		}

		public void TouchEnd(Point p)
		{
			var activeTools = Tools.Where(t => t.Active);
			foreach (var tool in activeTools) 
			{
				tool.TouchEnd(p);
			}
		}

		public void TouchMove(Point p)
		{
			var activeTools = Tools.Where(t => t.Active);
			foreach (var tool in activeTools) 
			{
				tool.TouchMove(p);
			}
		}
	}
}
