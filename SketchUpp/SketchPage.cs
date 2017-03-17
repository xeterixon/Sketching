using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Sketching.Helper;
using Sketching.Renderer;
using Sketching.Views;
using Xamarin.Forms;
using Sketching.Tool.Arrow;
using Sketching.Tool.Circle;
using Sketching.Tool.Mark;
using Sketching.Tool.Oval;
using Sketching.Tool.Rectangle;
using Sketching.Tool.Stroke;
using Sketching.Tool.Text;
using SketchUpp.CustomTool;

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
			_sketchView.CanDrawOutsideImageBounds = false;
			_sketchView.EnableGrid = false;

			// How to remove all tools
			//_sketchView.RemoveAllToolbarItems();

			// How to add all tools
			//_sketchView.AddAllToolbarItems();

			// How to remove selected tools
			//_sketchView.RemoveToolbarItemByIndex(2); // Highlight
			//_sketchView.RemoveToolbarItemByName(ToolNames.HighlightTool); // Highlight

			// Setup custom toolbar with title, colors and color descriptions
			_sketchView.AddToolbarItem(ImageSource.FromResource("SketchUpp.Resources.ruler.png", typeof(RulerTool.RulerTool).GetTypeInfo().Assembly), new RulerTool.RulerTool(Navigation), null);

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
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Line.png", typeof(LineTool).GetTypeInfo().Assembly), new LineTool(ToolNames.LineTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Curve.png", typeof(CurveTool).GetTypeInfo().Assembly), new CurveTool(ToolNames.CurveTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Highlight.png", typeof(HighlightTool).GetTypeInfo().Assembly), new HighlightTool(ToolNames.HighlightTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Circle.png", typeof(CircleTool).GetTypeInfo().Assembly), new CircleTool(ToolNames.CircleTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Oval.png", typeof(OvalTool).GetTypeInfo().Assembly), new OvalTool(ToolNames.OvalTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Rectangle.png", typeof(RectangleTool).GetTypeInfo().Assembly), new RectangleTool(ToolNames.RectangleTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Arrow.png", typeof(ArrowTool).GetTypeInfo().Assembly), new ArrowTool(ToolNames.ArrowTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Point.png", typeof(MarkTool).GetTypeInfo().Assembly), new MarkTool(ToolNames.PointTool, customToolbarName, customToolbarColors), null);
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Text.png", typeof(TextTool).GetTypeInfo().Assembly), new TextTool(Navigation, ToolNames.TextTool, customToolbarName, customToolbarColors), null);

			// Text with rounded corners if fill is active
			//_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Resources.Text.png", typeof(TextTool).GetTypeInfo().Assembly), new TextTool(Navigation, "RoundedFill", customToolbarName, customToolbarColors, true), null);

			// Create a custom MoistMeasure tool
			var moistColors = new List<KeyValuePair<string, Color>>
			{
				new KeyValuePair<string, Color>("MP1", Color.FromHex("#FBD447")),
				new KeyValuePair<string, Color>("MP2", Color.FromHex("#FFB678")),
				new KeyValuePair<string, Color>("MP3", Color.FromHex("#FFB678")),
				new KeyValuePair<string, Color>("MP4", Color.FromHex("#FF5149"))
			};
			var moistTool = new MoistTool("Moisture", "Mätpunkter", moistColors) { ShowDefaultToolbar = false };
			var assembly = typeof(SketchPage).GetTypeInfo().Assembly;
			_sketchView.AddToolbarItem(ImageSource.FromResource("SketchUpp.Resources.Moist.png", assembly), moistTool, null);
			GeometryRenderer.AddRenderer(new MoistRenderer());

			// Add default tools
			_sketchView.AddDefaultToolbarItems();

			// How to add the undo buttons
			//_sketchView.AddUndoTools();

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
				_sketchView.BackgroundImage = new BackgroundImage { Data = bytes };
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

