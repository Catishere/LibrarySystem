using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LibrarySystem.MVVM.Model.DB;
using Microsoft.Win32;

namespace LibrarySystem.Converter
{
    class ObjectPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not KeyValuePair<object, string> member) return string.Empty;
            if (value == null) return string.Empty;
            if (member.Key == null || member.Value == null) return string.Empty;
            if (member.Key.GetType().Name == "String") return value;
            var type = member.Key.GetType();
            var property = type.GetProperty(member.Value);
            return property?.GetValue(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string member) return null;
            if (parameter == null) return value;
            if (parameter is not KeyValuePair<object, string> field) return null;
            if (field.Key == null || field.Value == null) return null;
            if (field.Key.GetType().Name == "String") return value;
            object obj = field.Key;
            PropertyInfo propertyInfo = obj.GetType().GetProperty(field.Value);
            propertyInfo.SetValue(obj, member, null);
            return obj;
        }
    }
}
