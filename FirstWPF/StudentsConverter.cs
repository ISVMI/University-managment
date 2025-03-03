using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace FirstWPF
{
    public class StudentsConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            if (value is IEnumerable<Student> students) 
            {
                var result = string.Join(" ",students.Select(s =>$"{s.Surname} {s.Name}"));
                return result; 
            }
            return "Нет студентов";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
