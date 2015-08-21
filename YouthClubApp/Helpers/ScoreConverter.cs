using System;
using System.Globalization;
using System.Windows.Data;
using YouthClubApp.ViewModels;

namespace YouthClubApp.Helpers
{
    public class ScoreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vm = value as PlayerViewModel;
            var game = (string)parameter;

            var result = vm.GetScore(game);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This method should never be called");
        }
    }
}