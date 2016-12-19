using System;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Sketching.Common.Views;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class TextTool : ITool<IText>
	{
		private INavigation _navigation;
		public string Text {
			get { return Geometry.Value; }
			set { Geometry.Value = value; }
		}
		public TextTool(INavigation navigation)
		{
			_navigation = navigation;
			Name = "Text";
			Init();
		}

		public bool Active { get; set; }

		public IText Geometry { get; set; } = new Text();

		public string Name { get; set; }

		private void Init() 
		{
			Geometry = new Text(Geometry);
		}
		IGeometryVisual ITool.Geometry {
			get {
				return this.Geometry;
			}
			set {
				throw new NotImplementedException();
			}
		}

		public void TouchEnd(Point p)
		{
			Geometry.Point = p;
			Init();
		}

		public void TouchMove(Point p)
		{
			Geometry.Point = p;
		}

		public void TouchStart(Point p)
		{
			if (string.IsNullOrEmpty(Text))
			{
				var textInputView = new TextInputView();
				textInputView.TextEntryCompleted += (sender, text) =>
				{
					Text = text;
				};
				_navigation.PushAsync(new ContentPage { Content = textInputView });
			}
			else
			{
				Geometry.Point = p;
			}
		}
	}
}
