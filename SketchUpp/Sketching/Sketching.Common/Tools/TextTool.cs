using System;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Sketching.Common.Views;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class TextTool : ITextTool
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public IText Geometry { get; set; } = new Text();
		public bool CanUseFill { get; set; } = true;
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
		public TextTool(INavigation navigation)
		{
			_navigation = navigation;
			Name = "Text";
			Init();
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
				var textInputView = Helper.Factory.CreateTextInput(_navigation);
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
