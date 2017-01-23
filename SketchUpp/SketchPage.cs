using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Sketching.Helper;
using Sketching.Interfaces;
using Sketching.Views;
using Xamarin.Forms;
using Sketching.Tool.Arrow;
using Sketching.Tool.Circle;
using Sketching.Tool.Mark;
using Sketching.Tool.Oval;
using Sketching.Tool.Rectangle;
using Sketching.Tool.Stroke;
using Sketching.Tool.Text;

namespace SketchUpp
{
	public class SketchPage : ContentPage
	{
		private readonly SketchView _sketchView;
		public Command SaveCommand { get; set; }
		public bool TextToolIsActive { get; set; } = false;
		~SketchPage() 
		{
			System.Diagnostics.Debug.WriteLine("~SketchPage");
		}
		public SketchPage()
		{
			Title = "Sketching";
			SaveCommand = new Command(async () => { await SaveImage(); });
			_sketchView = new SketchView
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};
			_sketchView.SketchArea.CanDrawOutsideImageBounds = false;
			_sketchView.RemoveAllToolbarItems();
			//_sketchView.AddAllToolbarItems();

			// How to remove tools
			//_sketchView.RemoveToolbarItemByIndex(2); // Highlight
			//_sketchView.RemoveToolbarItemByName(ToolNames.HighlightTool); // Highlight

			// Setup custom toolbar with title, colors and color descriptions
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Arrow.png", typeof(ArrowTool).GetTypeInfo().Assembly), new RulerTool.RulerTool(), null);

			// How to add the undo buttons
			_sketchView.AddUndoTools();

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
			byte[] bytes;
			using (var s = new MemoryStream())
			{
				mf.GetStream().CopyTo(s);
				bytes = s.ToArray();
			}
			if (bytes != null)
			{
				_sketchView.SketchArea.BackgroundImage = new BackgroundImage { Data = bytes };
			}
		}
		private async Task SelectImage()
		{
			await CrossMedia.Current.Initialize();
			var img = await CrossMedia.Current.PickPhotoAsync();
			DecodeAndDrawMediaFile(img);

		}
		private async Task SaveImage()
		{
			var data = _sketchView.SketchArea.LargeImageData();
			var page = new SnapShotPage();
			page.SetImage(data);
			await Navigation.PushAsync(page);
		}
	}
}

