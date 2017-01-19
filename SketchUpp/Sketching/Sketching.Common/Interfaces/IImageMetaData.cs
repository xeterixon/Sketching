using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sketching.Interfaces
{
	public interface IImageMetaData
	{
		Task<Size> ImageSize(byte[] imageData);
	}
}
