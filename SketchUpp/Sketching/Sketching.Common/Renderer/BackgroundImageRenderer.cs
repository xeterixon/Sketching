using System;
using Sketching.Interfaces;
using SkiaSharp;

namespace Sketching.Renderer
{
	public class BackgroundImageRenderer : IRenderer
	{
		//NOTE This might be kept in memory a bit to long...
		private SKBitmap _scaledBitmap;
		private SKImage _scaledImage;
		private IImage _image;
		private int _lastClipWidth = -1;
		//NOTE The "ImageDisplay*" properties are used for clipping the drawing.... Doesn't work well, but well enough
		public int ImageDisplayWidth = int.MaxValue;
		public int ImageDisplayHeight =int.MaxValue;
		public IImage Image {
			get {
				return _image;
			}
			set {
				_scaledBitmap?.Dispose();
				_scaledBitmap = null;
				_image = value;
			}
		}
		public void Setup(SKCanvas canvas, double scale) { }
		private SKImage ResizeImage(SKRect canvasSize, byte[] data)
		{
			var bm = ResizeBitmap(canvasSize, data);
			//TODO Does this leak?
			return SKImage.FromBitmap(bm);
		}
		private SKBitmap ResizeBitmap(SKRect canvasSize, byte[] data)
		{
			using (var orgBitmap = SKBitmap.Decode(data)) {
				Image.Width  = orgBitmap.Width;
				Image.Height = orgBitmap.Height;
				var scale = Math.Min(canvasSize.Width / (double)orgBitmap.Width, canvasSize.Height / (double)orgBitmap.Height);
				var bm = new SKBitmap((int)(orgBitmap.Width * scale), (int)(orgBitmap.Height * scale));
				ImageDisplayWidth  = bm.Width ;
				ImageDisplayHeight = bm.Height;
				var canvas = new SKCanvas(bm);
				canvas.DrawBitmap(orgBitmap, new SKRect(0, 0, bm.Width, bm.Height));
				return bm;
			}
		}
		private void RenderImage(SKCanvas canvas) 
		{
			if (Image == null) return;
			if (_lastClipWidth != canvas.ClipDeviceBounds.Width || _scaledImage == null) {
				_scaledImage?.Dispose();
				_scaledImage = ResizeImage(canvas.ClipDeviceBounds, Image.Data);
				_lastClipWidth = canvas.ClipDeviceBounds.Width;
			}
			canvas.DrawImage(_scaledImage, 0, 0);

		}
		private void RenderBitmap(SKCanvas canvas) 
		{
			if (Image == null) return;
			if (_lastClipWidth != canvas.ClipDeviceBounds.Width || _scaledBitmap == null) 
			{
				_scaledBitmap?.Dispose();
				_scaledBitmap = ResizeBitmap(canvas.ClipDeviceBounds, Image.Data);
				_lastClipWidth = canvas.ClipDeviceBounds.Width;
			}
			canvas.DrawBitmap(_scaledBitmap, 0, 0);
		}
		public void Render(SKCanvas canvas, double scale) 
		{
			// Rendering a bitmap is a tad faster than rendering a image. Keeping the code, though
			RenderBitmap(canvas);
			//RenderImage(canvas);
		}
	}
}
