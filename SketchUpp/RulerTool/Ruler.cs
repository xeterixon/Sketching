﻿using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace SketchUpp.RulerTool
{
	public class Ruler : IRuler
	{
		public Ruler()
		{
			ToolSettings = new ToolSettings { SelectedColor = Color.Black };
		}
		public Ruler(IGeometryVisual v)
		{
			v.CopyTo(this);
		}
		public Color Color { get; set; } = Color.Black;

		public Point End { get; set; } = Point.Zero;

		public bool IsFilled { get; set; } = false;
		public bool IsStenciled { get; set; } = false;

		public bool IsValid { get { return Start != Point.Zero && End != Point.Zero; } }

		public double MaxSize { get; set; } = 20;

		public double MinSize { get; set; } = 1;

		public ToolSettings ToolSettings { get; set; }

		public double Size { get; set; } = 3;

		public Point Start { get; set; } = Point.Zero;
	}
}
