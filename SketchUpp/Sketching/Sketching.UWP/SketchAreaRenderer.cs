using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Sketching.Interfaces;
using Sketching.Views;
using Sketching.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using SKNativeView = SkiaSharp.Views.UWP.SKXamlCanvas;

[assembly: ExportRenderer(typeof(SketchArea), typeof(SketchAreaRenderer))]
namespace Sketching.UWP
{
	public class SketchAreaRenderer : ViewRenderer<SketchArea, SKNativeView>
	{

		protected override void OnElementChanged(ElementChangedEventArgs<SketchArea> e)
		{
			if (e.NewElement != null)
			{
				var view = new SKNativeView();
				view.PointerPressed += ViewOnPointerPressed;
				view.PointerMoved += ViewOnPointerMoved;
				view.PaintSurface += ViewOnPaintSurface;
				view.PointerReleased += ViewOnPointerReleased;

				e.NewElement.CallbackToNative = CallbackToNative;

				SetNativeControl(view);
				Control.Invalidate();
			}
			if (e.OldElement != null)
			{
				e.OldElement.CallbackToNative = null;
				// Control seems to be null most (all?) of the time on UWP.
				// Better safe than sorry, since it's not null on iOS and Droid.
				if (Control != null)
				{
					Control.PointerPressed -= ViewOnPointerPressed;
					Control.PointerMoved -= ViewOnPointerMoved;
					Control.PaintSurface -= ViewOnPaintSurface;
					Control.PointerReleased -= ViewOnPointerReleased;
				}
			}
			base.OnElementChanged(e);
		}

		private void ViewOnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
		{
			var point = GetPoint((UIElement)sender, pointerRoutedEventArgs);
			Element.TouchEnd(point);
			Control.Invalidate();
		}

		private void CallbackToNative(CallbackType callbackType)
		{
			if (callbackType == CallbackType.Repaint)
			{
				Control.Invalidate();
			}
		}

		private void ViewOnPaintSurface(object sender, SkiaSharp.Views.UWP.SKPaintSurfaceEventArgs skPaintSurfaceEventArgs)
		{
			Element.Draw(skPaintSurfaceEventArgs.Surface, skPaintSurfaceEventArgs.Info);
		}

		private void ViewOnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
		{
			if (!pointerRoutedEventArgs.Pointer.IsInContact)
				return;

			var point = GetPoint((UIElement)sender, pointerRoutedEventArgs);
			Element.TouchMove(point);

			Control.Invalidate();
		}

		private void ViewOnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
		{
			var point = GetPoint((UIElement)sender, pointerRoutedEventArgs);
			Element.TouchStart(point);
			Control.Invalidate();
		}

		private Point GetPoint(UIElement uiElement, PointerRoutedEventArgs pointerRoutedEventArgs)
		{
			var uwpPoint = pointerRoutedEventArgs.GetCurrentPoint(uiElement).Position;
			var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
			return new Point(uwpPoint.X * scale, uwpPoint.Y * scale);
		}
	}
}