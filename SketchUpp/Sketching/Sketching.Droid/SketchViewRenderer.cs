using Android.Runtime;
using Android.Util;
using Android.Views;
using Sketching.Common.Views;
using Sketching.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using SKNativeView = SkiaSharp.Views.Android.SKCanvasView;

[assembly: ExportRenderer(typeof(SketchArea), typeof(SketchViewRenderer))]
namespace Sketching.Droid
{
	public class TouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
	{
		SketchArea _view;
		public TouchListener(SketchArea view)
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
	public class SketchViewRenderer : ViewRenderer<SketchArea, SKNativeView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<SketchArea> e)
		{
			if (e.NewElement != null) 
			{
				
				var view = new SKNativeView(Forms.Context);
				view.SetOnTouchListener(new TouchListener(e.NewElement));
				view.PaintSurface+= Skia_PaintSurface;
				SetNativeControl(view);
				e.NewElement.CallbackToNative += CallbackToNative;
				SetNativeScreenSizeToElement();
				Control.Invalidate();
			}
			if (e.OldElement != null)
			{
				e.OldElement.CallbackToNative = null;
				if (Control != null)
				{
					Control.SetOnTouchListener(null);
					Control.PaintSurface -= Skia_PaintSurface;
				}
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
				//HACK Droid is not releaseing stuff as it should. This takes care of the crash in #25 but it leaks memory.
				Control?.Invalidate();
			}
		}

		void Skia_PaintSurface(object sender, SkiaSharp.Views.Android.SKPaintSurfaceEventArgs e)
		{
			Element?.Draw(e.Surface,e.Info);
		}
	}
}
