using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using YouthClubApp.Helpers;

namespace YouthClubApp.ViewModels
{
    public class ScoresViewModel : ViewModelBase, IPageViewModel
    {
        private readonly Lazy<ObservableCollection<ScoreViewModel>> scores;

        public ScoresViewModel()
        {
            ButtonCommand = new RelayCommand(OnClose);
        }

        public ScoresViewModel(IList<PlayerViewModel> players) : this()
        {
            Name = "Overall Scores";
            scores = new Lazy<ObservableCollection<ScoreViewModel>>(() =>
            {
                return new ObservableCollection<ScoreViewModel>(players.Select(p => new ScoreViewModel(p.Name, p.GetScore())).OrderByDescending(s => s.Score));
            });
        }

        public ScoresViewModel(string gameName, IList<PlayerViewModel> players) : this(players)
        {
            Name = gameName + " Scores";
            scores = new Lazy<ObservableCollection<ScoreViewModel>>(() =>
            {
                return new ObservableCollection<ScoreViewModel>(players.Select(p => new ScoreViewModel(p.Name, p.GetScore(gameName))).OrderByDescending(s => s.Score));
            });
        }

        public event EventHandler Close;

        public ICommand ButtonCommand { get; }
        public string Name { get; }

        public ObservableCollection<ScoreViewModel> Scores => scores.Value;

        private void OnClose(object parameter)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}