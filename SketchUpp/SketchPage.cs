using System;
using System.Reflection;
using Sketching.Common;
using Sketching.Common.Interfaces;
using Sketching.Common.Tools;
using Sketching.Common.Views;
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

			_sketchView.RemoveToolbarItem(new CircleTool().Name);
			
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

