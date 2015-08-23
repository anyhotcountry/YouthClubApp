using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YouthClubApp.Models;
using YouthClubApp.ViewModels;

namespace YouthClubApp.Views
{
    public partial class BadShooterView : UserControl
    {
        private Window window;

        public BadShooterView()
        {
            InitializeComponent();
            Unloaded += (o, e) => window.KeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as BadShooterViewModel;
            Point pt = CrossHairCanvas.TransformToAncestor(ParentGrid)
                                      .Transform(new Point(CrossHairCanvas.ActualWidth, CrossHairCanvas.ActualHeight));
            var rx = pt.X - (0.5 * ParentGrid.ActualWidth);
            var ry = pt.Y - (0.5 * ParentGrid.ActualHeight);
            var rsquared = (rx * rx) + (ry * ry);
            var outerRSquared = 0.25 * OuterEllipse.ActualWidth * OuterEllipse.ActualWidth;
            var innerRSquared = 0.25 * InnerEllipse.ActualWidth * InnerEllipse.ActualWidth;
            var bullseyeRSquared = 0.25 * BullsEye.ActualWidth * BullsEye.ActualWidth;
            var hitType = HitTypes.Miss;
            if (rsquared <= bullseyeRSquared)
            {
                hitType = HitTypes.BullsEye;
            }
            else if (rsquared <= innerRSquared)
            {
                hitType = HitTypes.Inner;
            }
            else if (rsquared <= outerRSquared)
            {
                hitType = HitTypes.Outer;
            }

            vm.OnShot(e.Key, hitType);
        }

        private void UserControlOnLoaded(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.KeyDown += OnKeyDown;
            var vm = DataContext as BadShooterViewModel;
            vm.Start();
        }
    }
}