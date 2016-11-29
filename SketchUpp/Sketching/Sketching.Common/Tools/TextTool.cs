using System;
using Sketching.Common.Geometries;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Tools
{
	public class TextTool : ITool<IText>
	{
		public string Text {
			get { return Geometry.Value; }
			set { Geometry.Value = value; }
		}
		public TextTool()
		{
			Name = "Text";
			Init();
		}

		public bool Active { get; set; }

		public IText Geometry { get; set; } = new Text();

		public string Name { get; set; }
		public  ToolType ToolType { get { return ToolType.Text; } }

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
			Geometry.Point = p;

		}
	}
}
