using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Windows.Input;
using Sketching.Common.Interfaces;
using Sketching.Common.Tools;
using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public partial class SketchView : ContentView
	{
		public Command<string> ActivateToolCommand { get; set; }
		public ICommand UndoCommand { get; set; }
		private Image _undoImage;

		public static readonly BindableProperty ToolCollectionProperty = BindableProperty.Create(nameof(ToolCollection), typeof(IToolCollection), typeof(SketchView), null,
  propertyChanged: ToolCollectionPropertyChanged);
		public IToolCollection ToolCollection
		{
			get { return (IToolCollection)GetValue(ToolCollectionProperty); }
			set { SetValue(ToolCollectionProperty, value); }
		}

		private static void ToolCollectionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
		}

		public SketchArea SketchArea => sketchArea;

		// ToolbarHeight
		public static readonly BindableProperty ToolbarHeightProperty = BindableProperty.Create(nameof(ToolbarHeight), typeof(double), typeof(SketchView), 50.0, propertyChanged: ToolbarHeightPropertyChanged);
		public double ToolbarHeight
		{
			get { return (double)GetValue(ToolbarHeightProperty); }
			set { SetValue(ToolbarHeightProperty, value); }
		}

		private static void ToolbarHeightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			SetToolbarHeight(((SketchView)bindable).toolbarStack, (double)newValue);
		}

		public SketchView()
		{
			InitializeComponent();
			ToolCollection = new ToolCollection();
			ActivateToolCommand = new Command<string>(ActivateTool);
			UndoCommand = new Command(() => { ToolCollection.Undo(); });

			AddDefaultToolbarItems();

			SetToolbarHeight(toolbarStack, ToolbarHeight);

			sketchArea.Delegates.Add(ToolCollection);
			sketchArea.ToolCollection = ToolCollection;
		}

		private void AddDefaultToolbarItems()
		{
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Line.png"), new LineTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Curve.png"), new CurveTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Circle.png"), new CircleTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Rectangle.png"), new RectangleTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Point.png"), new PointTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Text.png"), new TextTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Undo.png"), null, UndoCommand);
		}

		private void ActivateTool(string toolName)
		{
			ToolCollection.ActivateTool(toolName);

			// Unselect all items
			foreach (var child in toolbarStack.Children.Where(n => n is SketchToolbarItem))
			{
				var toolBarItem = ((SketchToolbarItem)child);
				toolBarItem.IsSelected = toolBarItem?.Tool?.Name == toolName;
			}
		}

		public void AddToolbarItem(ImageSource imageSource, ITool tool, ICommand command)
		{
			if (tool != null && ToolCollection.Tools.Any(n => n.Name == tool.Name))
				throw new VerificationException("Toolname already exists");

			var newSketchToolbarItem = new SketchToolbarItem(imageSource, tool, command ?? ActivateToolCommand)
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				WidthRequest = ToolbarHeight,
				IsSelected = toolbarStack.Children.Count == 0 // Select the first added tool
			};
			toolbarStack.Children.Add(newSketchToolbarItem);

			if (tool != null)
			{
				ToolCollection.Tools.Add(tool);
			}
		}

		public void RemoveAllToolbarItems()
		{
			List<string> toolNames = ToolCollection.Tools.Select(n => n.Name).ToList();
			foreach (string toolName in toolNames)
			{
				RemoveToolbarItem(toolName);
			}
			RemoveUndoToolbarItem();
		}

		public void RemoveToolbarItem(string toolName)
		{
			var toolbarItem = (SketchToolbarItem)toolbarStack.Children.FirstOrDefault(n => n is SketchToolbarItem && ((SketchToolbarItem)n).Tool.Name == toolName);
			if (toolbarItem == null) return;

			ToolCollection.Tools.Remove(toolbarItem.Tool);
			toolbarStack.Children.Remove(toolbarItem);
		}

		public void RemoveUndoToolbarItem()
		{
			var toolbarItem = (SketchToolbarItem)toolbarStack.Children.FirstOrDefault(n => n is SketchToolbarItem && ((SketchToolbarItem)n).Tool == null);
			if (toolbarItem != null)
			{
				toolbarStack.Children.Remove(toolbarItem);
			}
		}

		private static void SetToolbarHeight(StackLayout toolbarStack, double height)
		{
			toolbarStack.HeightRequest = height;
			foreach (var sketchToolbarItem in toolbarStack.Children)
			{
				sketchToolbarItem.WidthRequest = height;
			}
		}
	}
}
