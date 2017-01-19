using System;

namespace Sketching.Interfaces
{
	public interface IImage
	{
		byte[] Data { get; set; }
		double Width { get; set; }
		double Height { get; set; }
	}

	//TODO Move/Remove this...
	public class BackgroundImage : IImage
	{
		public byte[] Data { get; set;}
		public double Width { get; set; }
		public double Height { get; set; }
	}
}
