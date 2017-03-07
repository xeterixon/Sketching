using System;
using Sketching.Interfaces;

namespace Sketching.Extensions
{
	public static class GeometryVisualExtensions
	{
		public static void CopyTo(this IGeometryVisual self, IGeometryVisual copy) 
		{
			if (copy == null) return;
			copy.IsFilled = self.IsFilled;
			copy.IsStenciled = self.IsStenciled;
			copy.MaxSize = self.MaxSize;
			copy.MinSize = self.MinSize;
			copy.Size = self.Size;
			copy.ToolSettings = self.ToolSettings;
		}
	}
}
