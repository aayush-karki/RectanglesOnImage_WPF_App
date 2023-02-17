﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RectanglesOnImage_WPF_App
{ 
	public enum ToolEnum
	{
		// default tool, does nothing
		Hand,

		// when this tool is selected and a rectangle is pressed then changes the color of the rectangle
		Fill,

		// draws rectangle when click and dragged
		Rectangle
	}
}
