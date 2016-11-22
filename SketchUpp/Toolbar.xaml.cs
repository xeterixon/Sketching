using System;
using System.Collections.Generic;
using Sketching.Common.Tools;
using Xamarin.Forms;

namespace SketchUpp
{
	public partial class Toolbar : ContentView
	{
		public static readonly BindableProperty ToolCollectionProperty = BindableProperty.Create(nameof(ToolCollection), typeof(IToolCollection), typeof(Toolbar), null,
		  propertyChanged: (bindable, oldValue, newValue) => ((Toolbar)bindable).OnToolCollectionChanged(oldValue, newValue)
		);

		public IToolCollection ToolCollection {
			get { return (IToolCollection)GetValue(ToolCollectionProperty); }
			set { SetValue(ToolCollectionProperty, value); }
		}
		private void OnToolCollectionChanged(object oldValue, object newValue)
		{
			//TODO Do your stuff...
		}
		
		public Toolbar()
		{
			InitializeComponent();
		}
	}
}
