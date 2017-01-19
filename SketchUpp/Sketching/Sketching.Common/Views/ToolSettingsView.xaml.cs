using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Sketching.Tool;
using Xamarin.Forms;

namespace Sketching.Views
{
	public partial class ToolSettingsView : ContentPage
	{
		private StackOrientation _orientation;
		private List<KeyValuePair<string, Color>> _colorPalette;
		private List<KeyValuePair<string, Color>> _customColorPalette;
		private const double ColumnsInVertical = 3.0;
		private const double ColumnsInHorisontal = 5.0;
		public ICommand ColorSelectedCommand { get; set; }
		public ITool Tool { get; set; }

		public ToolSettingsView(ITool tool, StackOrientation orientation)
		{
			Tool = tool;
			ColorSelectedCommand = new Command<Color>(color =>
			{
				Tool.Geometry.Color = color;
				Navigation.PopAsync();
			});

			_orientation = orientation;
			InitializeComponent();
			BindingContext = this;

			thinLineImage.Source = ImageSource.FromResource("Sketching.Resources.ThinLine.png");
			thickLineImage.Source = ImageSource.FromResource("Sketching.Resources.ThickLine.png");
			customColorsLayout.IsVisible = false;
			CreateColorPalette(tool);
			SetupAndFillColorGrids();

			// Xamarin.Forms bug still not fixed https://bugzilla.xamarin.com/show_bug.cgi?id=31970
			// Andreas 2016-12-21
			if (Device.OS == TargetPlatform.Windows)
			{
				var toolInitSize = tool.Geometry.Size;
				Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
				{
					sizeSlider.Value = toolInitSize;
					return false;
				});
			}
		}

		private void CreateColorPalette(ITool tool)
		{
			if (tool?.CustomToolbarColors != null && tool.CustomToolbarColors.Any())
			{
				customColorsLayout.IsVisible = true;
				customColorsTitle.Text = tool.CustomToolbarName;
				_customColorPalette = tool.CustomToolbarColors.ToList();
			}
			_colorPalette = new List<KeyValuePair<string, Color>>
				{
					new KeyValuePair<string, Color>(string.Empty, Color.White),
					new KeyValuePair<string, Color>(string.Empty, Color.Silver),
					new KeyValuePair<string, Color>(string.Empty, Color.Gray),
					new KeyValuePair<string, Color>(string.Empty, Color.Black),
					new KeyValuePair<string, Color>(string.Empty, Color.Orange),
					new KeyValuePair<string, Color>(string.Empty, Color.Yellow),
					new KeyValuePair<string, Color>(string.Empty, Color.Aqua),
					new KeyValuePair<string, Color>(string.Empty, Color.Blue),
					new KeyValuePair<string, Color>(string.Empty, Color.Navy),
					new KeyValuePair<string, Color>(string.Empty, Color.Lime),
					new KeyValuePair<string, Color>(string.Empty, Color.Green),
					new KeyValuePair<string, Color>(string.Empty, Color.Teal),
					new KeyValuePair<string, Color>(string.Empty, Color.Fuchsia),
					new KeyValuePair<string, Color>(string.Empty, Color.Red),
					new KeyValuePair<string, Color>(string.Empty, Color.Purple)
				};
		}

		private void OnSizeChanged(object sender, EventArgs e)
		{
			_orientation = Width > Height ? StackOrientation.Horizontal : StackOrientation.Vertical;
			if (Device.Idiom == TargetIdiom.Desktop) return;
			SetupAndFillColorGrids();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			SizeChanged += OnSizeChanged;
		}

		private void SetupAndFillColorGrids()
		{
			// Custom colors
			var numberOfRowsInCustomColorGrid = SetupColorGridAndReturnNumberOfRows(_customColorPalette, customColorsGrid);
			FillColorGrid(_customColorPalette, customColorsGrid);
			// Default colors
			var numberOfRowsInDefaultColorGrid = SetupColorGridAndReturnNumberOfRows(_colorPalette, colorGrid);
			FillColorGrid(_colorPalette, colorGrid);
			// Height ratio of the two color grids
			if (_customColorPalette != null && _customColorPalette.Any() && numberOfRowsInCustomColorGrid > 0)
			{
				CalculatePaletteGridHeights(numberOfRowsInDefaultColorGrid, numberOfRowsInCustomColorGrid);
			}
		}

		private int SetupColorGridAndReturnNumberOfRows(List<KeyValuePair<string, Color>> palette, Grid grid)
		{
			var numberOfRows = 0;
			if (palette == null || !palette.Any()) return numberOfRows;
			if (grid.ColumnDefinitions.Any()) grid.ColumnDefinitions.Clear();
			if (grid.RowDefinitions.Any()) grid.RowDefinitions.Clear();
			if (grid.Children.Any()) grid.Children.Clear();
			var numberOfColors = palette.Count;
			if (_orientation == StackOrientation.Vertical)
			{
				numberOfRows = (int)Math.Ceiling(numberOfColors / ColumnsInVertical);
				for (var i = 0; i < numberOfRows; i++)
				{
					grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				}
				for (var i = 0; i < ColumnsInVertical; i++)
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				}
			}
			else
			{
				numberOfRows = (int)Math.Ceiling(numberOfColors / ColumnsInHorisontal);
				for (var i = 0; i < numberOfRows; i++)
				{
					grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				}
				for (var i = 0; i < ColumnsInHorisontal; i++)
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				}
			}
			return numberOfRows;
		}

		private void FillColorGrid(List<KeyValuePair<string, Color>> palette, Grid grid)
		{
			if (palette == null || !palette.Any()) return;
			if (grid.ColumnDefinitions.Any()) grid.ColumnDefinitions.Clear();
			if (grid.RowDefinitions.Any()) grid.RowDefinitions.Clear();
			if (grid.Children.Any()) grid.Children.Clear();
			var i = 1;
			var left = -1;
			var top = 0;
			var timeForNewRowInVertical = (int)ColumnsInVertical + 1;
			var timeForNewRowInHorisontal = (int)ColumnsInHorisontal + 1;
			foreach (var label in palette.Select(item => new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 12.0,
				LineBreakMode = LineBreakMode.TailTruncation,
				Text = item.Key,
				TextColor = GetTextColor(item.Value),
				BackgroundColor = item.Value
			}))
			{
				var tapGestureRecognizer = new TapGestureRecognizer
				{
					Command = ColorSelectedCommand,
					CommandParameter = label.BackgroundColor
				};

				label.GestureRecognizers.Add(tapGestureRecognizer);

				left++;
				if (_orientation == StackOrientation.Vertical)
				{
					if (i == timeForNewRowInVertical) // New line
					{
						left = 0;
						top++;
						timeForNewRowInVertical += (int)ColumnsInVertical;
					}
				}
				else
				{
					if (i == timeForNewRowInHorisontal) // New line
					{
						left = 0;
						top++;
						timeForNewRowInHorisontal += (int)ColumnsInHorisontal;
					}
				}
				grid.Children.Add(label, left, top);
				i++;
			}
		}

		private void CalculatePaletteGridHeights(int numberOfRowsInDefaultColorGrid, int numberOfRowsInCustomColorGrid)
		{
			if (paletteGrid.ColumnDefinitions.Any()) paletteGrid.ColumnDefinitions.Clear();
			if (paletteGrid.RowDefinitions.Any()) paletteGrid.RowDefinitions.Clear();
			if (paletteGrid.Children.Any()) paletteGrid.Children.Clear();

			// Calculate the height of each palette
			var customColorGridHeight = 1.0;
			var defaultColorGridHeight = 1.0;
			if (numberOfRowsInDefaultColorGrid > numberOfRowsInCustomColorGrid)
			{
				var ratio = (double)numberOfRowsInCustomColorGrid / numberOfRowsInDefaultColorGrid;
				customColorGridHeight = 1 - ratio;
			}
			if (numberOfRowsInDefaultColorGrid < numberOfRowsInCustomColorGrid)
			{
				var ratio = (double)numberOfRowsInDefaultColorGrid / numberOfRowsInCustomColorGrid;
				defaultColorGridHeight = 1 - ratio;
			}

			paletteGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(customColorGridHeight, GridUnitType.Star) });
			paletteGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(defaultColorGridHeight, GridUnitType.Star) });
			paletteGrid.Children.Add(customColorsLayout, 0, 0);
			paletteGrid.Children.Add(colorGrid, 0, 1);
		}

		private static Color GetTextColor(Color backgroundColor)
		{
			var backgroundColorDelta = ((backgroundColor.R * 0.3) + (backgroundColor.G * 0.6) + (backgroundColor.B * 0.1));
			return (backgroundColorDelta > 0.4f) ? Color.Black : Color.White; // Returns black or white text depending on the delta channel
		}
	}
}
