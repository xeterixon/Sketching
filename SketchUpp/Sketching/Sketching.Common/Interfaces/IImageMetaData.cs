using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IImageMetaData
	{
		Task<Size> ImageSize(byte[] imageData);
	}
}
