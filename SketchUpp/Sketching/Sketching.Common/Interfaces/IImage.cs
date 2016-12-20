namespace Sketching.Common.Interfaces
{
	public interface IImage
	{
		byte[] Data { get; set; }
	}

	//TODO Move/Remove this...
	public class BackgroundImage : IImage
	{
		public byte[] Data { get; set;}
	}
}
