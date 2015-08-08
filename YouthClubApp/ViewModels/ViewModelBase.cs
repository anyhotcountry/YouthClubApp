using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace YouthClubApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        bool? closeWindowFlag;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool? CloseWindowFlag
        {
            get
            {
                return closeWindowFlag;
            }

            set
            {
                closeWindowFlag = value;
                OnPropertyChanged(nameof(CloseWindowFlag));
            }
        }

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CloseWindowFlag = !(CloseWindowFlag ?? false);
            }));
        }

        internal void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
