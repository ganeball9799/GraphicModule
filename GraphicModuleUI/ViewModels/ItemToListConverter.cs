using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GraphicModuleUI.ViewModels
{
    public class ItemToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null ? new List<object> { value } : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
