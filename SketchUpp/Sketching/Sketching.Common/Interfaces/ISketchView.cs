﻿using System;
namespace Sketching.Interfaces
{
	public enum CallbackType 
	{
		Repaint,
	}
	public interface ISketchView
	{
		//TODO Rename? It's not that visible to the end user, though...
		Action<CallbackType> CallbackToNative { get; set; }
		IImage ImageData();
	}
}
