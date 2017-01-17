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
		public Command<ITool> ActivateToolCommand { get; set; }
		public ICommand UndoCommand { get; set; }
		public ICommand UndoAllCommand { get; set; }

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
			ActivateToolCommand = new Command<ITool>(ActivateTool);
			UndoCommand = new Command(() => { ToolCollection.Undo(); });
			UndoAllCommand = new Command(() => { ToolCollection.UndoAll(); });

			AddDefaultToolbarItems();

			SetToolbarHeight(toolbarStack, ToolbarHeight);

			sketchArea.Delegates.Add(ToolCollection);
			sketchArea.ToolCollection = ToolCollection;
		}

		private void AddDefaultToolbarItems()
		{
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Line.png"), new LineTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Curve.png"), new CurveTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Highlight.png"), new HighlightTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Circle.png"), new CircleTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Oval.png"), new OvalTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Rectangle.png"), new RectangleTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Arrow.png"), new ArrowTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Point.png"), new PointTool(), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Text.png"), new TextTool(Navigation), ActivateToolCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Undo.png"), null, UndoCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Trash.png"), null, UndoAllCommand);
		}

		private void ActivateTool(ITool tool)
		{
			if (SelectedTool?.Name == tool?.Name)
			{
				OpenToolSettings(SelectedTool);
				return;
			}

			ToolCollection.ActivateTool(tool);

			// Unselect all items
			foreach (var child in toolbarStack.Children.Where(n => n is SketchToolbarItem))
			{
				var toolBarItem = (SketchToolbarItem)child;
				toolBarItem.IsSelected = toolBarItem?.Tool == tool;
			}
		}

		private ITool SelectedTool
		{
			get
			{
				var selectedItem = toolbarStack.Children.FirstOrDefault(n => (n as SketchToolbarItem)?.IsSelected == true);

				return ((SketchToolbarItem)selectedItem)?.Tool;
			}
		}

		private void OpenToolSettings(ITool tool)
		{
			var orientation = Width > Height ? StackOrientation.Horizontal : StackOrientation.Vertical;
			Navigation.PushAsync(new ToolSettingsView(tool, orientation));
		}

		public void AddToolbarItem(ImageSource imageSource, ITool tool, ICommand command)
		{
			if (tool != null && ToolCollection.Tools.Any(n => n.Name == tool.Name))
				throw new VerificationException("Tool already exists");

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
				if (newSketchToolbarItem.IsSelected)
				{
					ToolCollection.ActivateTool(tool);
				}
			}
		}

		public void AddAllToolbarItems()
		{
			RemoveAllToolbarItems();
			AddDefaultToolbarItems();
		}

		public void AddUndoTools()
		{
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Undo.png"), null, UndoCommand);
			AddToolbarItem(ImageSource.FromResource("Sketching.Common.Resources.Trash.png"), null, UndoAllCommand);
		}

		public void RemoveAllToolbarItems()
		{
			while (toolbarStack.Children.Any())
			{
				RemoveToolbarItemByIndex(0);
			}
		}

		public void RemoveToolbarItemByName(string name)
		{
			var toolsToRemove = new List<SketchToolbarItem>();
			foreach (var child in toolbarStack.Children)
			{
				var toolbarItem = child as SketchToolbarItem;
				if (toolbarItem?.Tool == null) continue;
				if (!toolbarItem.Tool.Name.Equals(name)) continue;
				toolsToRemove.Add(toolbarItem);
			}
			foreach (var toolbarItem in toolsToRemove)
			{
				if (ToolCollection.Tools.Contains(toolbarItem.Tool))
					ToolCollection.Tools.Remove(toolbarItem.Tool);
				if (toolbarStack.Children.Contains(toolbarItem))
					toolbarStack.Children.Remove(toolbarItem);
			}
		}

		public void RemoveToolbarItemByIndex(int index)
		{
			var toolbarItem = (SketchToolbarItem)toolbarStack.Children[index];
			if (toolbarItem == null) return;

			if (ToolCollection.Tools.Contains(toolbarItem.Tool))
			{
				ToolCollection.Tools.Remove(toolbarItem.Tool);
			}

			if (toolbarStack.Children.Contains(toolbarItem))
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
