using System.Windows;
using YouthClubApp.ViewModels;
using YouthClubApp.Views;

namespace YouthClubApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var app = new ApplicationView();
            var context = new ApplicationViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}