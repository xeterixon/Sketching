using System;
using System.Collections.Generic;
using System.Reflection;
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

			// How to remove tools
			_sketchView.RemoveToolbarItem(3); // Circle
			_sketchView.RemoveToolbarItem(2); // Highlight

			// How to add custom tools
			_sketchView.AddToolbarItem(null, new OvalTool(), null);
			_sketchView.AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Highlight.png", typeof(CurveTool).GetTypeInfo().Assembly), new CurveTool("Fuktpunkter", 50, 100, 0.3, new List<Color> {Color.Red, Color.Orange, Color.Yellow}), null);

			ToolbarItems.Add(new ToolbarItem { Text = "Save", Command = SaveCommand });
			Content = _sketchView;
		}

		private void SaveImage() 
		{
			var data = _sketchView.SketchArea.ImageData();
			Navigation.PushAsync(new SnapShotPage(data));
		}
	}
}

