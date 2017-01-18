﻿using System;
using System.Linq;
using Sketching.Common.Views;
using Sketching.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SKNativeView = SkiaSharp.Views.iOS.SKCanvasView;

[assembly : ExportRenderer(typeof(SketchArea),typeof(SketchViewRenderer))]
namespace Sketching.iOS
{
	public class TouchGestureRecognizer : UIGestureRecognizer 
	{
		SketchArea _view;
		UIView _native;
		nfloat scale;
		public TouchGestureRecognizer(SketchArea view, UIView native) 
		{
			_view = view;
			_native = native;
			scale = UIScreen.MainScreen.Scale;
		}
		private bool TryGetAnyTouch(Foundation.NSSet touches, ref Point p) 
		{
			try {
				UITouch touch = (UITouch)touches.AnyObject;
				var loc = touch.LocationInView(touch.View);
				p.X = loc.X * scale;
				p.Y = loc.Y * scale;
				return true;

			} catch(Exception ex)
			{
				System.Diagnostics.Debug.Write(ex.Message);
			}
			return false;
			
		}
		public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			var point = new Point();
			if (TryGetAnyTouch(touches, ref point)) 
			{
				_view.TouchStart(point);
				_native.SetNeedsDisplay();
			}
		}
		public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			var point = new Point();
			if (TryGetAnyTouch(touches, ref point)) {
				_view.TouchEnd(point);
				_native.SetNeedsDisplay();
			}

		}
		public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			var point = new Point();
			if (TryGetAnyTouch(touches, ref point)) {
				_view.TouchMove(point);
				_native.SetNeedsDisplay();
			}

		}

	}
	public class SketchViewRenderer : ViewRenderer<SketchArea,SKNativeView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<SketchArea> e)
		{
			if (e.OldElement != null)
			{
				e.OldElement.CallbackToNative = null;
				if (Control != null)
				{
					var gestureRecognizers = Control.GestureRecognizers.Where(g => g.GetType() == typeof (TouchGestureRecognizer)).ToList();
					foreach (var gestureRecognizer in gestureRecognizers)
					{
						Control.RemoveGestureRecognizer(gestureRecognizer);
					}
					Control.PaintSurface -= Skia_PaintSurface;
				}
			}
			if (e.NewElement != null) {
				e.NewElement.CallbackToNative = HandleCallback;
				var view = new SKNativeView();
				view.PaintSurface += Skia_PaintSurface;
				view.AddGestureRecognizer(new TouchGestureRecognizer(e.NewElement, view));
				SetNativeControl(view);
				view.SetNeedsDisplay(); // INITIAL Paint.
			}
			base.OnElementChanged(e);			
		}


		void HandleCallback(Common.Interfaces.CallbackType obj)
		{
			if (obj == Common.Interfaces.CallbackType.Repaint) 
			{
				Control?.SetNeedsDisplay();
			}
		}

		void Skia_PaintSurface(object sender, SkiaSharp.Views.iOS.SKPaintSurfaceEventArgs e)
		{
			Element.Draw(e.Surface,e.Info);
		}
	}
}
