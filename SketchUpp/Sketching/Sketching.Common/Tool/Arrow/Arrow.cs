﻿using Sketching.Extensions;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;

namespace Sketching.Tool.Arrow
{
	public class Arrow : IArrow
	{
		public Arrow() : this(new ToolSettings { SelectedColor = Color.Black }, 8, false) { }
		public Arrow(IGeometryVisual src) : this() 
		{
			src.CopyTo(this);
		}
		public Arrow(ToolSettings toolSettings, double size, bool isFilled)
		{
			Start = new Point(-1, -1);
			End = new Point(-1, -1);
			ToolSettings = toolSettings;
			IsFilled = isFilled;
			Size = size;
			MinSize = 1;
			MaxSize = 20;
		}

		public bool IsValid => Start.X > 0 && End.X > 0 && Start != End;
		public double Size { get; set; }
		public ToolSettings ToolSettings { get; set; }
		public bool IsFilled { get; set; }
		public bool IsStenciled { get; set; } = false;
		public double MinSize { get; set; }
		public double MaxSize { get; set; }
		public Point Start { get; set; }
		public Point End { get; set; }
	}
}
