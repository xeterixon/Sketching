using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sketching.Common.Extensions;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Render
{
	public static class GeometryRenderer
	{
		static GeometryRenderer()
		{
			_renderers = new List<IRenderer>();

			AddRenderer(new StrokeRenderer());
			AddRenderer(new CircleRenderer());
			AddRenderer(new RectangleRenderer());
			AddRenderer(new TextRenderer());
			AddRenderer(new MarkRenderer());
		}
		private static List<IRenderer> _renderers;
		public static void AddRenderer(IRenderer renderer) 
		{
			var existing = _renderers.Where((arg) => arg.GeometryType.GetTypeInfo().IsAssignableFrom(renderer.GetType().GetTypeInfo()));
			if (existing.Any()) 
			{
				System.Diagnostics.Debug.WriteLine("A renderer exist allready");
			}
			_renderers.Add(renderer);
		}
		public static void Render(SKCanvas canvas, IGeometryVisual geometry)
		{
			if (!geometry.IsValid) return;
			var renderer = _renderers.FirstOrDefault((arg) => arg.GeometryType.GetTypeInfo().IsAssignableFrom( geometry.GetType().GetTypeInfo()));
			if (renderer != null) renderer.Render(canvas, geometry);

		}

	}
}
