using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanglesOnImage_WPF_App
{
	/// <summary>
	/// Indicates current selected tool
	/// </summary>
	public enum ToolEnum
	{
		// default tool, move and resize rectangle
		Hand,

		// when this tool is selected and a rectangle is pressed then changes the color of the rectangle
		Fill,

		// draws rectangle when click and dragged
		Rectangle
	}
}
