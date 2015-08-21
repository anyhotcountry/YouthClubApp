using System;
using System.Windows.Data;

namespace YouthClubApp.Helpers
{
    public class RelativeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((Double)values[0] * (Double)values[1] * 0.01) - (values.Length > 2 ? (Double)values[2] : 0);
            }
            catch (Exception)
            {
                // Ignore
            }

            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}