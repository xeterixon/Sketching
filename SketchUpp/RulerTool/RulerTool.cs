using System;
using System.Collections.Generic;
using Sketching.Helper;
using Sketching.Interfaces;
using Sketching.Tool;
using Sketching.Tool.Text;
using Sketching.Views;
using Xamarin.Forms;

namespace SketchUpp.RulerTool
{
	public class RulerTool : ITool<IRuler>
	{
		// Temp points used to position the text
		Point lastEnd = Point.Zero;
		Point lastStart = Point.Zero;
		bool isInputingText = false;
		INavigation _navigation;
		public RulerTool(INavigation navigation)
		{
			_navigation = navigation;
			Geometry = new Ruler();
		}

		public bool Active { get; set;}

		public bool CanUseFill {get; set; }

		public IEnumerable<KeyValuePair<string, Color>> CustomToolbarColors {get; set; }

		public string CustomToolbarName {get; set; }

		public IRuler Geometry {get; set; }

		public string Name {get; set; }

		public bool ShowDefaultToolbar { get; set; } = true;

		IGeometryVisual ITool.Geometry {
			get {
				return Geometry;
			}

			set {
				throw new NotSupportedException();
			}
		}

		public void TouchEnd(Point p)
		{
			System.Diagnostics.Debug.WriteLine(lastStart.ToString());
			if (isInputingText) return;
			isInputingText = true;
			Geometry.End = p;
			lastEnd = p;
			Geometry = new Ruler(Geometry);
			// Hook up a text input
			var textInputView = Factory.CreateTextInput(_navigation);
			textInputView.Begin();
			textInputView.TextEntered += (sender, text) => {
				var t = new Text(new ToolPaletteItem { ItemColor = Color.White }, 45, true) 
				{
					Point = new Point((lastEnd.X + lastStart.X) / 2, (lastEnd.Y + lastStart.Y) / 2),
					Value = text
				};
				isInputingText = false;
				lastStart = Point.Zero;
				lastEnd = Point.Zero;
				((ITextInput)sender).End();
				MessagingCenter.Send(new AddGeometryMessage(t), nameof(AddGeometryMessage));
			};


		}

		public void TouchMove(Point p)
		{
			Geometry.End = p;
		}

		public void TouchStart(Point p)
		{
			isInputingText = false;
			lastStart = p;
			Geometry.Start = p;	
		}
	}
}
