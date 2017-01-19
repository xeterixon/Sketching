using System;
using Sketching.Views;

namespace Sketching
{
	public static class Bootstrap
	{
		public static void Init() 
		{
			Helper.Factory.RegisterTextInput(typeof(TextInputView));
		}
	}
}
