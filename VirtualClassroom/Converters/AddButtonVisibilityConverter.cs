﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace VirtualClassroom.Converters
{
	public class AddButtonVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return System.Windows.Visibility.Visible;
			}

			return System.Windows.Visibility.Hidden;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
