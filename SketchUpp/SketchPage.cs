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
		private Toolbar _toolbar;
		public Command<string> ActivateToolCommand { get; set; }
		public Command UndoCommand { get; set; }
		public SketchPage() 
		{
			UndoCommand = new Command(Undo);
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
			var sketchView = new SketchView 
			{
				VerticalOptions		= LayoutOptions.FillAndExpand,
				HorizontalOptions	= LayoutOptions.FillAndExpand,

			};
			sketchView.Delegates.Add(_toolbar.ToolCollection);
			sketchView.ToolCollection = _toolbar.ToolCollection;
			var stack = new StackLayout {
				Children =
				{
					_toolbar,
					sketchView
				}
			};
			Content = stack;
		}
		private void Undo() 
		{
			_toolbar.ToolCollection.Undo();
		}
		private void ActivateTool(string toolname) 
		{
			_toolbar.ToolCollection.ActivateTool(toolname);
		}
	}
}

