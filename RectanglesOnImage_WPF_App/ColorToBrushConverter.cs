using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RectanglesOnImage_WPF_App
{
	/// <summary>
	/// defines a converter that converts color to solid color brush
	/// </summary>
	class ColorToSolidColorBrushConverter : IValueConverter
	{
		public object Convert( object value , Type targetType , object parameter , CultureInfo culture )
		{
			return new SolidColorBrush((Color)value);
		}

		public object ConvertBack( object value , Type targetType , object parameter , CultureInfo culture )
		{
			throw new NotImplementedException();
		}
	}
}
