using System;
using System.Reflection;
using Sketching.Common;
using Sketching.Common.Tools;
using Sketching.Common.Views;
using SkiaSharp;
using Xamarin.Forms;

namespace SketchUpp
{
	public class SketchPage : ContentPage
	{
		private string _text;
		public string Text 
		{
			get { return _text;}
			set
			{
				_text = value;
				if (TextToolIsActive) 
				{
					var tool =_toolbar.ToolCollection.ActiveTool as TextTool;
					if (tool == null) return;
					tool.Text = _text;
				}
				OnPropertyChanged(nameof(Text));
			}
		}
		private Toolbar _toolbar;
		private SketchView _sketchView;
		public Command<string> ActivateToolCommand { get; set; }
		public Command UndoCommand { get; set; }
		public Command SaveCommand { get; set; }
		public bool TextToolIsActive { get; set; } = false;
		public SketchPage() 
		{
			Title = "Sketching";
			UndoCommand = new Command(Undo);
			SaveCommand = new Command(SaveImage);
			ActivateToolCommand = new Command<string>(ActivateTool);
			_toolbar = new Toolbar {
				ToolCollection = new ToolCollection(),
				BindingContext = this
			};
			_toolbar.ToolCollection.Tools.Add(new LineTool { Active = true });
			_toolbar.ToolCollection.Tools.Add(new CurveTool { Active = false });
			_toolbar.ToolCollection.Tools.Add(new PointTool { Active = false });
			_toolbar.ToolCollection.Tools.Add(new CircleTool { Active = false });
			_toolbar.ToolCollection.Tools.Add(new RectangleTool { Active = false });
			_toolbar.ToolCollection.Tools.Add(new TextTool { Active = false });
			_sketchView = new SketchView 
			{
				VerticalOptions		= LayoutOptions.FillAndExpand,
				HorizontalOptions	= LayoutOptions.FillAndExpand,

			};
			_sketchView.Delegates.Add(_toolbar.ToolCollection);
			_sketchView.ToolCollection = _toolbar.ToolCollection;
			var stack = new StackLayout {
				Children =
				{
					_toolbar,
					_sketchView
				}
			};
			ToolbarItems.Add(new ToolbarItem { Text = "Save", Command = SaveCommand });
			Content = stack;

		}
		private void Undo() 
		{
			_toolbar.ToolCollection.Undo();
		}
		private void SaveImage() 
		{
			var data = _sketchView.ImageData();
			Navigation.PushAsync(new SnapShotPage(data));
		}
		private void ActivateTool(string toolname) 
		{
			_toolbar.ToolCollection.ActivateTool(toolname);
			TextToolIsActive = false;
			if ("Text" == toolname) 
			{
				TextToolIsActive = true;
			}
			OnPropertyChanged(nameof(TextToolIsActive));
		}
	}
}

