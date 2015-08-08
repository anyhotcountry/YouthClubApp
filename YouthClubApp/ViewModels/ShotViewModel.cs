namespace YouthClubApp.ViewModels
{
    public class ShotViewModel : ViewModelBase
    {
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
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged(nameof(X));
                }
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
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }
    }
}