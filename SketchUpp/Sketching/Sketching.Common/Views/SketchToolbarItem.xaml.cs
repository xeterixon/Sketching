using System.Windows.Input;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public partial class SketchToolbarItem : ContentView
	{
		public static readonly BindableProperty SelectionColorProperty = BindableProperty.Create(nameof(SelectionColor), typeof(Color), typeof(SketchToolbarItem), Color.Orange);
		public Color SelectionColor
		{
			get { return (Color)GetValue(SelectionColorProperty); }
			set { SetValue(SelectionColorProperty, value); }
		}

		public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SketchToolbarItem), false, propertyChanged: IsSelectedPropertyChanged);
		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		private static void IsSelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if ((bool)newValue)
			{
				((SketchToolbarItem)bindable).toolLine.BackgroundColor = ((SketchToolbarItem)bindable).SelectionColor;
			}
			else
			{
				((SketchToolbarItem)bindable).toolLine.BackgroundColor = Color.Transparent;
			}
		}

		public ITool Tool { get; set; }

		public SketchToolbarItem(ImageSource imageSource, ITool tool, ICommand tappedCommand)
		{
			InitializeComponent();

			toolImage.Source = imageSource;
			toolImage.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = tappedCommand,
				CommandParameter = tool
			});

			Tool = tool;
		}
	}
}
