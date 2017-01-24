using System;
using Sketching.Interfaces;

namespace Sketching.Helper
{
	public class AddGeometryMessage
	{
		public IGeometryVisual Geometry { get; private set;}
	
		public AddGeometryMessage(IGeometryVisual g) 
		{
			Geometry = g;
		}
	}

	public class RepaintMessage
	{

	}

}
