using System;
using System.Windows.Input;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public partial class SketchToolbarItem : ContentView
	{
		public ITool Tool { get; set; }

		public SketchToolbarItem(ImageSource imageSource, ITool tool, ICommand tappedCommand )
		{
			InitializeComponent();

			toolImage.Source = imageSource;
			toolImage.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = tappedCommand,
				CommandParameter = tool.Name
			});

			Tool = tool;
		}
	}
}
