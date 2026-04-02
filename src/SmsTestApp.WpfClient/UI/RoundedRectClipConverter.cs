using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SmsTestApp.WpfClient.UI
{
    /// <summary>
    /// Конвертер для обрезки содержимого элемента до скругленного прямоугольника, используя его ширину, высоту и радиус скругления.
    /// </summary>
    public class RoundedRectClipConverter : IMultiValueConverter
    {
        /// <inheritdoc/>
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null || values.Length < 3)
            {
                return null;
            }

            if (values[0] is not double width || values[1] is not double height || values[2] is not CornerRadius cornerRadius)
            {
                return null;
            }

            var radius = cornerRadius.TopLeft;
            return new RectangleGeometry(new Rect(0d, 0d, width, height), radius, radius);
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}