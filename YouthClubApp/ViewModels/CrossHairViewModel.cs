namespace YouthClubApp.ViewModels
{
    public class CrossHairViewModel : ViewModelBase
    {
        private double x;
        private double y;

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
