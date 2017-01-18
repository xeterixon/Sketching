using System;
using System.Reflection;
using Sketching.Interfaces;
using Xamarin.Forms;

namespace Sketching.Helper
{
	//TODO Look into having some kind of "real" DI/ServiceLocator thingy. This is (to?) quick-n-dirty
	public static class Factory
	{
		static Type _textInputType;
		public static ITextInput CreateTextInput(INavigation nav) 
		{
			var input = (ITextInput)Activator.CreateInstance(_textInputType);
			input.NavigationProxy = nav;
			return input;
		}
		public static void RegisterTextInput(Type t) 
		{
			if (typeof(ITextInput).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo())) {
				_textInputType = t;
			} else 
			{
				throw new TypeLoadException($"{t.Name} does not implement ITextInput");
			}
		}
	}
}
