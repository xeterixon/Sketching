using System;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Sketching.Common.Views;
using Sketching.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using SKNativeView = SkiaSharp.Views.Android.SKCanvasView;

[assembly: ExportRenderer(typeof(SketchView), typeof(SketchViewRenderer))]
namespace Sketching.Droid
{
	public class TouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
	{
		SketchView _view;
		public TouchListener(SketchView view)
		{
			_view = view;
		}
		public bool OnTouch(Android.Views.View v, MotionEvent e)
		{
			bool retval = false;
			if (e.Action == MotionEventActions.Down) 
			{
				_view.TouchStart(new Point(e.GetX(), e.GetY()));
				retval = true;
			}
			if (e.Action == MotionEventActions.Move) 
			{
				_view.TouchMove(new Point(e.GetX(), e.GetY()));
				retval = true;
			}
			if (e.Action == MotionEventActions.Cancel || e.Action == MotionEventActions.Up) 
			{
				_view.TouchEnd(new Point(e.GetX(), e.GetY()));
				retval = true;
			}
			if (retval) 
			{
				v.Invalidate();
			}
			return retval;
		}
	}
	public class SketchViewRenderer : ViewRenderer<SketchView, SKNativeView>, Android.Views.View.IOnTouchListener
	{

		protected override void OnElementChanged(ElementChangedEventArgs<SketchView> e)
		{
			if (e.NewElement != null) 
			{
				
				var view = new SKNativeView(Xamarin.Forms.Forms.Context);
				view.SetOnTouchListener(new TouchListener(e.NewElement));
				view.PaintSurface+= Skia_PaintSurface;
				SetNativeControl(view);
				e.NewElement.CallbackToNative += CallbackToNative;
				SetNativeScreenSizeToElement();
				Control.Invalidate();
			}
			base.OnElementChanged(e);
		}
		private void SetNativeScreenSizeToElement() 
		{
			if (Element == null) return;
			var winservice = Context.GetSystemService(global::Android.Content.Context.WindowService).JavaCast<IWindowManager>();
			DisplayMetrics metrics1 = new DisplayMetrics();
			winservice.DefaultDisplay.GetRealMetrics(metrics1);

		}

		void CallbackToNative(Common.Interfaces.CallbackType obj)
		{
			if (obj == Common.Interfaces.CallbackType.Repaint) {
				Control.Invalidate();
			}
		}

		void Skia_PaintSurface(object sender, SkiaSharp.Views.Android.SKPaintSurfaceEventArgs e)
		{
			Element?.Draw(e.Surface,e.Info);
		}
	}
}
