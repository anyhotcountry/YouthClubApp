using System;

namespace YouthClubApp.ViewModels
{
    public class ShotViewModel : ViewModelBase
    {
        private const double Tolerance = 0;
        private double x;
        private double y;

        public string Colour { get; internal set; }

        public double X
        {
            get
            {
                return x;
            }

            set
            {
                if (!(Math.Abs(x - value) > Tolerance))
                {
                    return;
                }

                x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public double Y
        {
            get
            {
                return y;
            }

            set
            {
                if (!(Math.Abs(y - value) > Tolerance))
                {
                    return;
                }

                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
    }
}