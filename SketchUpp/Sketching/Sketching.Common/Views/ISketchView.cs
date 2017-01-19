using System;
using Sketching.Interfaces;
namespace Sketching.Views
{
	public enum CallbackType 
	{
		Repaint,
	}
	public interface ISketchArea
	{
		//TODO Rename? It's not that visible to the end user, though...
		Action<CallbackType> CallbackToNative { get; set; }
		IImage ImageData();
	}
}
