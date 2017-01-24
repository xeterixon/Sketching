using Sketching.Interfaces;
using Sketching.Tool.Arrow;
using Sketching.Tool.Circle;
using Sketching.Tool.Mark;
using Sketching.Tool.Oval;
using Sketching.Tool.Rectangle;
using Sketching.Tool.Stroke;
using Sketching.Tool.Text;

namespace Sketching.Tool
{
	public interface ITool<T> : ITool
		where T : IGeometryVisual
	{
		new T Geometry { get; set; }
	}
	public interface ITool : ITouchDelegate, ICustomColorSetup
	{
		string Name { get; set; }
		bool Active { get; set; }
		IGeometryVisual Geometry { get; set; }
		bool CanUseFill { get; set; }
	}

	public interface IStrokeTool : ITool<IStroke> { } // This interface is used by all stroke tools, line, curve, highlight etc
	public interface IArrowTool : ITool<IArrow> { }
	public interface ICircleTool : ITool<ICircle> { }
	public interface IOvalTool : ITool<IOval> { }
	public interface IMarkTool : ITool<IMark> { }
	public interface IRectangleTool : ITool<IRectangle> { }
	public interface ITextTool : ITool<IText> { }
}
