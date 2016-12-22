﻿using System;

namespace Sketching.Common.Interfaces
{
	public interface IImage
	{
		byte[] Data { get; set; }
		double Width { get; set; }
	}

	//TODO Move/Remove this...
	public class BackgroundImage : IImage
	{
		public byte[] Data { get; set;}
		public double Width { get; set; }
	}
}
