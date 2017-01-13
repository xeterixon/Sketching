using System;
using System.Collections.Generic;
using Sketching.Common.Geometries;
using Sketching.Common.Helper;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class TextTool : ITextTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IText Geometry { get; set; } = new Text();
		public bool CanUseFill { get; set; } = true;
		public IEnumerable<Color> CustomColors { get; set; }

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
		public TextTool(INavigation navigation) : this(navigation, ToolNames.TextTool, 20, 200, 75, null) { }

		public TextTool(INavigation navigation, string name, double minSize, double maxSize, double startSize, IEnumerable<Color> customColors)
		{
			_navigation = navigation;
			Name = name;
			Geometry.MinSize = minSize;
			Geometry.MaxSize = maxSize;
			Geometry.Size = startSize;
			CustomColors = customColors;
		}


		private void Init()
		{
			Geometry = new Text(Geometry);
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
					Init();
					MessagingCenter.Send((object)this, "Repaint");

				};
			}
		}
	}
}
