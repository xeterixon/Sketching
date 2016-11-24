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
			ToolType = ToolType.Text;
			Init();
		}

		public bool Active { get; set; }

		public IText Geometry { get; set; } = new Text();

		public string Name { get; set; }

		public ToolType ToolType { get; set; }
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

		public void Activate()
		{
			Active = true;
		}

		public void Deactivate()
		{
			Active = false;
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
