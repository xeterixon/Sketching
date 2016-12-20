using System;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Renderer
{
	public class BackgroundImageRenderer : IRenderer
	{
		private SKBitmap _bgBitmap;
		private IImage _image;
		public IImage Image {
			get {
				return _image;
			}
			set {
				_bgBitmap?.Dispose();
				_bgBitmap = null;
				_image = value;
			}
		}
		public void Setup(SKCanvas canvas) { }
		public void Render(SKCanvas canvas) 
		{
			if (Image == null) return;
			if (_bgBitmap == null) {
				_bgBitmap = SKBitmap.Decode(Image.Data);
			}
			var scale = Math.Min(canvas.ClipDeviceBounds.Width / (double)_bgBitmap.Width, canvas.ClipDeviceBounds.Height / (double)_bgBitmap.Height);
			canvas.DrawBitmap(_bgBitmap, SKRect.Create((float)(_bgBitmap.Width * scale), (float)(_bgBitmap.Height * scale)));
			
		}
	}
}
