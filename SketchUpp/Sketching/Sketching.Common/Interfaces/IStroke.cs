﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sketching.Common.Interfaces
{
	public interface IStroke : IGeometryVisual
	{
		List<Point> Points { get; set; }
	}
}
