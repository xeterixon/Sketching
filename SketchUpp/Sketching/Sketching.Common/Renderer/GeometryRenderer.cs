using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sketching.Interfaces;
using SkiaSharp;
using Sketching.Tool.Arrow;
using Sketching.Tool.Circle;
using Sketching.Tool.Mark;
using Sketching.Tool.Oval;
using Sketching.Tool.Rectangle;
using Sketching.Tool.Stroke;
using Sketching.Tool.Text;

namespace Sketching.Renderer
{
	public static class GeometryRenderer
	{
		static GeometryRenderer()
		{
			_renderers = new List<IGeometryRenderer>();

			AddRenderer(new ArrowRenderer());
			AddRenderer(new StrokeRenderer());
			AddRenderer(new CircleRenderer());
			AddRenderer(new OvalRenderer());
			AddRenderer(new RectangleRenderer());
			AddRenderer(new TextRenderer());
			AddRenderer(new MarkRenderer());
		}

		//NOTE This should probably be a faster look-up-able container than a List, but it works for now...
		private static List<IGeometryRenderer> _renderers;
		public static void AddRenderer(IGeometryRenderer renderer)
		{
			var existing = _renderers.Where(arg => arg.GeometryType.GetTypeInfo().IsAssignableFrom(renderer.GetType().GetTypeInfo()));
			if (existing.Any())
			{
				throw new InvalidOperationException("Duplicate renderer");
			}
			_renderers.Add(renderer);
		}

		public static void RemoveRenderer(IGeometryRenderer renderer)
		{
			var r = _renderers.FirstOrDefault(arg => arg.GeometryType == renderer.GeometryType);
			if (r != null)
			{
				_renderers.Remove(r);
			}
		}

		public static void Render(SKCanvas canvas, IGeometryVisual geometry, double scale = 1.0)
		{
			if (!geometry.IsValid) return;
			var renderer = _renderers.FirstOrDefault(arg => arg.GeometryType.GetTypeInfo().IsAssignableFrom(geometry.GetType().GetTypeInfo()));
			renderer?.Render(canvas, geometry, scale);
		}
	}
}
