namespace Sketching.Common.Interfaces
{
	public interface ITool<T> : ITool
		where T : IGeometryVisual
	{
		new T Geometry { get; set; }
	}
	public interface ITool : ITouchDelegate
	{
		string Name { get; set; }
		bool Active { get; set; }
		IGeometryVisual Geometry { get; set; }
		bool CanUseFill { get; set; }
	}

	public interface IStrokeTool : ITool<IStroke> { }
	public interface ILineTool : ITool<ILine> { }
	public interface IArrowTool : ITool<IArrow> { }
	public interface ICircleTool : ITool<ICircle> { }
	public interface IOvalTool : ITool<IOval> { }
	public interface IPointTool : ITool<IMark> { }
	public interface IRectangleTool : ITool<IRectangle> { }
	public interface ITextTool : ITool<IText> { }
}
