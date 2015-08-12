using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace YouthClubApp.ViewModels
{
    public class ScoresViewModel : ViewModelBase, IPageViewModel
    {
        private string gameName;

        public ScoresViewModel(PlayerViewModel[] players)
        {
            this.Name = "Overall Scores";
            Scores = new ObservableCollection<ScoreViewModel>(players.Select(p => new ScoreViewModel(p.Name, p.GetScore())).OrderByDescending(s => s.Score));
        }

        public ScoresViewModel(string gameName, PlayerViewModel[] players) : this(players)
        {
            this.Name = gameName + "Scores";
            Scores = new ObservableCollection<ScoreViewModel>(players.Select(p => new ScoreViewModel(p.Name, p.GetScore(gameName))).OrderByDescending(s => s.Score));
        }

        public string Name { get; }

        public ObservableCollection<ScoreViewModel> Scores { get; }

        public event EventHandler Close;

        public void OnKeyDown(Key key)
        {
        }
    }
}
