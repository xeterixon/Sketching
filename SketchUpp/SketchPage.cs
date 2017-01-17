using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Sketching.Common.Helper;
using Sketching.Common.Interfaces;
using Sketching.Common.Tools;
using Sketching.Common.Views;
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
			var customToolbarName = "Fuktmarkering";
			var customToolbarColors = new List<KeyValuePair<string, Color>>
			{
				new KeyValuePair<string, Color>("Dry", Color.FromHex("#FBD447")),
				new KeyValuePair<string, Color>("Moist", Color.FromHex("#FFB678")),
				new KeyValuePair<string, Color>("Wet", Color.FromHex("#FF5149")),
				new KeyValuePair<string, Color>("Leaking place", Color.FromHex("#54A7D4")),
				new KeyValuePair<string, Color>("Rot", Color.FromHex("#4BD47B"))
			};
			// How to add custom tools
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Line.png", typeof(LineTool).GetTypeInfo().Assembly), new LineTool(ToolNames.LineTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Curve.png", typeof(CurveTool).GetTypeInfo().Assembly), new CurveTool(ToolNames.CurveTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Highlight.png", typeof(HighlightTool).GetTypeInfo().Assembly), new HighlightTool(ToolNames.HighlightTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Circle.png", typeof(CircleTool).GetTypeInfo().Assembly), new CircleTool(ToolNames.CircleTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Oval.png", typeof(OvalTool).GetTypeInfo().Assembly), new OvalTool(ToolNames.OvalTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Rectangle.png", typeof(RectangleTool).GetTypeInfo().Assembly), new RectangleTool(ToolNames.RectangleTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Arrow.png", typeof(ArrowTool).GetTypeInfo().Assembly), new ArrowTool(ToolNames.ArrowTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Point.png", typeof(PointTool).GetTypeInfo().Assembly), new PointTool(ToolNames.PointTool, customToolbarName, customToolbarColors), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Text.png", typeof(TextTool).GetTypeInfo().Assembly), new TextTool(Navigation, ToolNames.TextTool, customToolbarName, customToolbarColors), null);

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

