﻿using System;
using System.Collections.Generic;
using Sketching.Helper;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Oval
{
	public class OvalTool : IOvalTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IOval Geometry { get; set; } = new Oval();
		public bool CanUseFill { get; set; } = true;
		public bool CanUseStencil { get; set; } = true;


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

		/// <summary>
		/// OvalTool with default values
		/// </summary>
		public OvalTool() : this(ToolNames.OvalTool, 1, 20, 8, string.Empty, null) { }

		/// <summary>
		/// Customized OvalTool with default sizes
		/// </summary>
		public OvalTool(string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors) : this(name, 1, 20, 8, customToolbarName, customToolbarColors) { }

		/// <summary>
		/// Custom made OvalTool
		/// </summary>
		public OvalTool(string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors)
		{
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomToolbarName = customToolbarName;
			CustomToolbarColors = customToolbarColors;
		}

		public void TouchEnd(Point p)
		{
			Geometry.End = p;
			Geometry = new Oval(Geometry);
		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}

		public void TouchStart(Point p)
		{
			Geometry.Start = p;
		}

		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
		public bool ShowDefaultToolbar { get; set; } = true;
	}
}
