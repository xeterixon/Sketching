using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Sketching.Common;
using Sketching.Common.Interfaces;
using Sketching.Common.Tools;
using Sketching.Common.Views;
using SketchUpp.CustomTool;
using SkiaSharp;
using Xamarin.Forms;

namespace SketchUpp
{
	public class SketchPage : ContentPage
	{
		private readonly SketchView _sketchView;
		public Command SaveCommand { get; set; }
		public bool TextToolIsActive { get; set; } = false;
		public SketchPage() 
		{
			Title = "Sketching";
			SaveCommand = new Command(SaveImage);
			_sketchView = new SketchView 
			{
				VerticalOptions		= LayoutOptions.FillAndExpand,
				HorizontalOptions	= LayoutOptions.FillAndExpand,
			};
			_sketchView.SketchArea.CanDrawOutsideImageBounds = false;

			// How to remove tools
			_sketchView.RemoveToolbarItem(3); // Circle
			_sketchView.RemoveToolbarItem(2); // Highlight

			// How to add custom tools
			_sketchView.AddToolbarItem(null, new OvalTool(), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Highlight.png", typeof(CurveTool).GetTypeInfo().Assembly), new CurveTool("Fuktpunkter", 50, 100, 0.3, new List<Color> {Color.Red, Color.Orange, Color.Yellow}), null);

			ToolbarItems.Add(new ToolbarItem { Text = "Save", Command = SaveCommand });
			ToolbarItems.Add(new ToolbarItem { Text = "Photo", Command = new Command(async () => { await TakePhoto(); }) });
			ToolbarItems.Add(new ToolbarItem { Text = "Album", Command = new Command(async () => { await SelectImage(); }) });
			Content = _sketchView;
		}
		private async Task TakePhoto() 
		{
			await CrossMedia.Current.Initialize();
			if (CrossMedia.Current.IsCameraAvailable) 
			{
				var img = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions 
				{ 
					DefaultCamera = CameraDevice.Rear,
					Name = "Background image"
				});
				DecodeAndDrawMediaFile(img);

			}
		}
		private void DecodeAndDrawMediaFile(MediaFile mf) 
		{
			if (mf == null) return;
			byte[] bytes = null;
			using (var s = new MemoryStream())
			{
				mf.GetStream().CopyTo(s);
				bytes = s.ToArray();
			}
			if (bytes != null) {
				_sketchView.SketchArea.BackgroundImage = new BackgroundImage { Data = bytes };
			}
		}
		private async Task SelectImage() 
		{
			await CrossMedia.Current.Initialize();
			var img = await CrossMedia.Current.PickPhotoAsync();
			DecodeAndDrawMediaFile(img);

		}
		private void SaveImage() 
		{
			var data = _sketchView.SketchArea.LargeImageData();
			Navigation.PushAsync(new SnapShotPage(data));
		}
	}
}

