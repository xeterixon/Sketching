using System;
using Sketching.Common.Interfaces;
using SkiaSharp;

namespace Sketching.Common.Renderer
{
	public class BackgroundImageRenderer : IRenderer
	{
		private SKBitmap _scaledBitmap;
		private SKImage _scaledImage;
		private IImage _image;
		private int _lastClipWidth = -1;
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
		public void Setup(SKCanvas canvas) { }
		private SKImage ResizeImage(SKRect canvasSize, byte[] data)
		{
			var bm = ResizeBitmap(canvasSize, data);
			return SKImage.FromBitmap(bm);
		}
		private SKBitmap ResizeBitmap(SKRect canvasSize, byte[] data)
		{
			using (var orgBitmap = SKBitmap.Decode(data)) {
				var scale = Math.Min(canvasSize.Width / (double)orgBitmap.Width, canvasSize.Height / (double)orgBitmap.Height);
				var bm = new SKBitmap((int)(orgBitmap.Width * scale), (int)(orgBitmap.Height * scale));
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
		public void Render(SKCanvas canvas) 
		{
			// Rendering a bitmap is a tad faster than rendering a image. Keeping the code, though
			RenderBitmap(canvas);
			//RenderImage(canvas);
		}
	}
}
