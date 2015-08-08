using System;
using System.Windows.Input;

namespace YouthClubApp.ViewModels
{
    public class ScoresViewModel : ViewModelBase, IPageViewModel
    {
        private string gameName;
        private PlayerViewModel[] players;

        public ScoresViewModel(PlayerViewModel[] players)
        {
            this.players = players;
        }

        public ScoresViewModel(string gameName, PlayerViewModel[] players) : this(players)
        {
            this.gameName = gameName;
        }

        public string Name { get; } = "Scores";

        public event EventHandler Close;

        public void OnKeyDown(Key key)
        {
        }
    }
}
