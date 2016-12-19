using System;
namespace Sketching.Common.Interfaces
{
	public interface IImage
	{
		double Width { get; set; }
		double Height { get; set; }
		byte[] Data { get; set; }
	}

	//TODO Move this...
	public class BackgroundImage : IImage
	{
		public byte[] Data { get; set;}

		public double Height { get; set; }

		public double Width { get; set; }
	}
}
