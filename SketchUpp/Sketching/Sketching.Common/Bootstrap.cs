using System;
using Sketching.Common.Views;

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
