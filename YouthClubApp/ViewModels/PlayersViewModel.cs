using System;
using System.Collections.ObjectModel;

namespace YouthClubApp.ViewModels
{
    public class PlayersViewModel : ViewModelBase, IPageViewModel
    {
        public PlayersViewModel(ObservableCollection<PlayerViewModel> players)
        {
            Players = players;
        }

        public event EventHandler Close;

        public string Name => "Setup players";

        public ObservableCollection<PlayerViewModel> Players { get; }
    }
}