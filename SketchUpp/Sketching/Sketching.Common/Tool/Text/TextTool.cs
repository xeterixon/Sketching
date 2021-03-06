using System;
using System.Collections.Generic;
using Sketching.Helper;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Tool.Text
{
	public class TextTool : ITextTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IText Geometry { get; set; } = new Text();
		public bool CanUseFill { get; set; } = true;
		public bool CanUseStencil { get; set; } = false;

		public string CustomToolbarName { get; set; }
		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors { get; set; }
		public bool ShowDefaultToolbar { get; set; } = true;

		public string Text
		{
			get { return Geometry.Value; }
			set { Geometry.Value = value; }
		}

		IGeometryVisual ITool.Geometry
		{
			get
			{
				return Geometry;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		private INavigation _navigation;
		/// <summary>
		/// TextTool with default values
		/// </summary>
		public TextTool(INavigation navigation) : this(navigation, ToolNames.TextTool, 20, 200, 75, string.Empty, null) { }

		/// <summary>
		/// Customized TextTool with default sizes
		/// </summary>
		public TextTool(INavigation navigation, string name, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors, bool roundedFill = false) : this(navigation, name, 20, 200, 75, customToolbarName, customToolbarColors, roundedFill) { }

		/// <summary>
		/// Custom made TextTool
		/// </summary>
		public TextTool(INavigation navigation, string name, double minSize, double maxSize, double startSize, string customToolbarName, IEnumerable<KeyValuePair<string, Color>> customToolbarColors, bool roundedFill = false)
		{
			_navigation = navigation;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			Geometry.RoundedFill = roundedFill;
			CustomToolbarName = customToolbarName;
			CustomToolbarColors = customToolbarColors;
		}

		public void TouchEnd(Point p)
		{
			Geometry.Point = p;
		}

		public void TouchMove(Point p)
		{
			Geometry.Point = p;
		}

		public void TouchStart(Point p)
		{
			Geometry.Point = p;
			if (string.IsNullOrEmpty(Text))
			{
				var textInputView = Factory.CreateTextInput(_navigation);
				textInputView.Begin();
				textInputView.TextEntered += (sender, text) =>
				{
					Text = text;
					((ITextInput)sender).End();
					CreateNewGeometry();
					MessagingCenter.Send(new RepaintMessage(), nameof(RepaintMessage));
				};
			}
		}

		private void CreateNewGeometry()
		{
			var roundedFill = Geometry.RoundedFill;
			Geometry = new Text(Geometry) { RoundedFill = roundedFill };
		}
	}
}
