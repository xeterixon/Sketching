using System;
using System.Collections.Generic;
using System.Linq;
using Sketching.Common.Extensions;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Sketching.Common.Tools;
using SkiaSharp;
using Xamarin.Forms;
namespace Sketching.Common.Views
{
	
	public class SketchView : View, ITouchDelegate, ISketchView
	{
		//TODO Is this a bit overly designed? We only got one delegate and thats the ToolCollection object....
		public List<ITouchDelegate> Delegates { get; set; } = new List<ITouchDelegate>();
		private IToolCollection _tools;
		private SKSurface _surface;
		private byte[] _imageData;
		public IToolCollection ToolCollection {
			get { return _tools; }
			set 
			{
				_tools = value;
				_tools.View = this;
			}
		}
		private int _lastCanvasWidth = -1;
		public byte[] ImageData() 
		{
			return _imageData;
		}
		public Action<CallbackType> CallbackToNative { get; set; }

		public virtual void Draw(SKSurface surface, SKImageInfo info) 
		{
			_surface = surface;
			if (_lastCanvasWidth != (int)surface.Canvas.ClipBounds.Width) 
			{
				_lastCanvasWidth = (int)surface.Canvas.ClipBounds.Width;
				SetupGrid(surface.Canvas);
			}
			surface.Canvas.Clear(SKColors.AliceBlue);
			var canvas = surface.Canvas;
			if (ToolCollection == null) return;
			DrawGrid(canvas);
			//This is really not that object oriented... So kill me...
			foreach (var geom in ToolCollection.Geometries) 
			{
				if (!geom.IsValid) continue;
				if (geom is IStroke)	{ DrawStroke(canvas,(IStroke)geom);}
				if (geom is ICircle)	{ DrawCircle(canvas, (ICircle)geom);}
				if (geom is IMark)		{ DrawMark(canvas, (IMark)geom);}
				if (geom is IRectangle)	{ DrawRectangle(canvas, (IRectangle)geom); }
				if (geom is IText)		{ DrawText(canvas, (IText)geom);}
			}
			//TODO Try to do this on demand, rather than very draw...
			_imageData = surface.Snapshot().Encode(SKImageEncodeFormat.Jpeg, 100).ToArray();
		}
		private void DrawText(SKCanvas canvas, IText text) 
		{
			using (var paint = new SKPaint()) 
			{
				paint.Color = text.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.TextSize = (float)text.Size;
				canvas.DrawText(text.Value, (float)text.Point.X, (float)text.Point.Y, paint);
			}
		}
		private void DrawRectangle(SKCanvas canvas, IRectangle rect) 
		{
			using (var paint = new SKPaint()) 
			{
				paint.Color = rect.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)rect.Size;
				canvas.DrawRect(new SKRect((float)rect.Start.X, (float)rect.Start.Y, (float)rect.End.X, (float)rect.End.Y), paint);
					
			}
		}
		private void DrawCircle(SKCanvas canvas, ICircle circle) 
		{
			using (var paint = new SKPaint()) 
			{
				paint.Color = circle.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = true;
				paint.StrokeWidth = (float)circle.Size;
				canvas.DrawCircle((float)circle.Start.X, (float)circle.Start.Y, (float)circle.Radius, paint);
			}
		}
		private void DrawMark(SKCanvas canvas, IMark mark) 
		{
			using (var paint = new SKPaint()) 
			{
				paint.Color = mark.Color.ToSkiaColor();
				paint.IsAntialias = true;
				paint.IsStroke = false;
				paint.StrokeWidth = (float)mark.Size;
				paint.StrokeCap = SKStrokeCap.Round;
				canvas.DrawPoint((float)mark.Point.X,(float) mark.Point.Y, paint);
				
			}
		}
		private void SetupGrid(SKCanvas canvas) 
		{
			//NOTE The canvas arg is not used yet....
			grid.Clear();
			// try to get roughly 15 vertical lines in portrait, rounding to the nearest 10 pixel
			var theLength = Math.Min(canvas.ClipBounds.Width, canvas.ClipBounds.Height);
			if (Config.GridSize < 0) 
			{
				Config.GridSize = ((((int)theLength / 15) + 5) / 10) * 10;
			}
			var baseStroke = new Stroke { Size = 1, Color = new Color(0, 0, 0, 0.4) };
			int counter = 0;
			do {
				var stroke = new Stroke(baseStroke);
				stroke.Points.Add(new Point { X = counter * Config.GridSize, Y = 0 });
				stroke.Points.Add(new Point { X = counter * Config.GridSize, Y = canvas.ClipBounds.Height });
				grid.Add(stroke);
				counter++;
			} while ((counter * Config.GridSize) < canvas.ClipBounds.Width);
			counter = 0;
			do {
				var stroke = new Stroke(baseStroke);
				stroke.Points.Add(new Point { X = 0, Y = counter*Config.GridSize });
				stroke.Points.Add(new Point { X = canvas.ClipBounds.Width, Y = counter *Config.GridSize });
				grid.Add(stroke);
				counter++;

			} while (counter * Config.GridSize < canvas.ClipBounds.Height);
		}
		private List<Stroke> grid = new List<Stroke>();
		private void DrawGrid(SKCanvas canvas) 
		{
			//TODO Look into double buffer this so we don't have to draw it every time.
			foreach (var stroke in grid) 
			{
				DrawStroke(canvas,stroke);
			}
		}
		private void DrawStroke(SkiaSharp.SKCanvas canvas, IStroke s) 
		{
			// Got to have more than one point to draw a line
			if (!s.IsValid) return;
			using (var paint = new SKPaint()) 
			{
				paint.IsStroke = true;
				paint.StrokeCap = SKStrokeCap.Round;
				paint.StrokeWidth = (float)s.Size;
				paint.IsAntialias = true;
				paint.Color = s.Color.ToSkiaColor();
				var first = s.Points.First();
				var theRest = s.Points.Skip(1);
				foreach (var point in theRest) 
				{
					canvas.DrawLine((float)first.X, (float)first.Y, (float)point.X, (float)point.Y, paint);
					first = point;
				}
			}
		}
		public virtual void TouchStart(Point p) 
		{
			foreach (var item in Delegates) 
			{
				item.TouchStart(p);
			}
		}
		public virtual void TouchEnd(Point p) 
		{
			foreach (var item in Delegates) {
				item.TouchEnd(p);
			}
		}
		public virtual void TouchMove(Point p) 
		{
			foreach (var item in Delegates) {
				item.TouchMove(p);
			}
		}
	}
}

