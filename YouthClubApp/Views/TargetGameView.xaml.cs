using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YouthClubApp.Models;
using YouthClubApp.ViewModels;

namespace YouthClubApp.Views
{
    public partial class TargetGameView : UserControl
    {
        public TargetGameView()
        {
            InitializeComponent();
        }

        private void UserControlOnLoaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as TargetGameViewModel;
            Point pt = CrossHairCanvas.TransformToAncestor(ParentGrid)
                                      .Transform(new Point(-0.5 * CrossHairCanvas.ActualWidth, -0.5 * CrossHairCanvas.ActualHeight));
            var centreX = 0.5 * ParentGrid.ActualWidth;
            var centreY = 0.5 * ParentGrid.ActualHeight;
            var rx = pt.X - centreX;
            var ry = pt.Y - centreY;
            var rsquared = rx * rx + ry * ry;
            var outerRSquared = 0.25 * OuterEllipse.Width * OuterEllipse.Width;
            var innerRSquared = 0.25 * InnerEllipse.Width * InnerEllipse.Width;
            var bullseyeRSquared = 0.25 * BullsEye.Width * BullsEye.Width;
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
    }
}
