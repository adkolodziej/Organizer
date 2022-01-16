using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Organizer.Converters
{
	public class DayBackgroundColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isFiltered = (bool)value;

			if (isFiltered == true)
			{
				return new LinearGradientBrush(Color.FromRgb(101, 198, 148), Color.FromRgb(80, 164, 30), new Point(0.5, 0), new Point(0.5, 1));
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
