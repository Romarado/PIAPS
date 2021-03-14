using System;
using System.Windows.Data;
using System.Globalization;

namespace Stanok.Converters
{
    public class CheckBoxEnumConverter : IValueConverter
    {
        /// <summary>
        /// Конвертация соответствия значения перечисления (value) параметру (parameter) 
        /// Используется для формализации правила CheckBox.IsChecked = (value == parameter)
        /// /// </summary>
        /// <param name="value"> Поле типа enum </param>
        /// <param name="targetType"> Выходной тип - bool? </param>
        /// <param name="parameter"> Требуемое значение enum'a </param>
        /// <returns> True - если value соответсвует требуемуму значению param, False - иначе, null - в случае ошибки </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.GetType().IsEnum == true &&
                parameter?.GetType().IsEnum == true &&
                value.GetType().IsAssignableFrom(parameter.GetType()))
            {
                return value.Equals(parameter);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Обратная конвертация
        /// </summary>
        /// <param name="value"> CheckBox.IsChecked - bool? </param>
        /// <param name="targetType"> Перечисление enum </param>
        /// <param name="parameter"> Требуемое значение enum'a </param>
        /// <returns> Значение, которое будет присвоено свойсвту, для которого используется Binding с этим конвертером</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as bool?) == true &&
                parameter?.GetType().IsAssignableFrom(targetType) == true)
            {
                return parameter;
            }
            else
            {
                return Enum.GetValues(targetType).GetValue(0);
            }
        }
    }
}
