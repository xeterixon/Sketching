﻿using System;
using Android.Graphics;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Droid.Helper
{
	public class ImageMetaData : IImageMetaData
	{
		public Size ImageSize(byte[] imageData)
		{
			try {
				var options = new BitmapFactory.Options {
					InJustDecodeBounds = true
				};
				var image = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);
				var size = new Size(options.OutWidth, options.OutHeight);
				image?.Dispose();
				return size;
			} catch (Exception e) {
				Console.WriteLine("Failed to load imageData " + e.Message);
			}
			return Size.Zero;
		}
	}
	
}
