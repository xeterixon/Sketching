using System;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IImageMetaData
	{
		Size ImageSize(byte[] imageData);
	}
}
