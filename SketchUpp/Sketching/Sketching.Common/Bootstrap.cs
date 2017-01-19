using System;
using Sketching.Views;

namespace Sketching.Common
{
	public static class Bootstrap
	{
		public static void Init() 
		{
			Helper.Factory.RegisterTextInput(typeof(TextInputView));
		}
	}
}
