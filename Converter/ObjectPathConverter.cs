using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LibrarySystem.MVVM.Model.DB;

namespace LibrarySystem.Converter
{
    class ObjectPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null) return value;
            if (parameter is not string member) return "";
            return value.GetType().GetProperty(member).GetValue(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string member) return null;
            if (parameter is not string field) return null;
            Book book = new Book();
            PropertyInfo propertyInfo = book.GetType().GetProperty(field);
            propertyInfo.SetValue(book, member, null);
            return book;
        }
    }
}
