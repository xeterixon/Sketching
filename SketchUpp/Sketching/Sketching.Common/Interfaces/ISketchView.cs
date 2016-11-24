using System;
namespace Sketching.Common.Interfaces
{
	public enum CallbackType 
	{
		Repaint,
	}
	public interface ISketchView
	{
		Action<CallbackType> CallbackToNative { get; set; }
		byte[] ImageData();
	}
}
