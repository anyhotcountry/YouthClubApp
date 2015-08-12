namespace YouthClubApp.ViewModels
{
    public class ScoreViewModel : ViewModelBase
    {
        public ScoreViewModel(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public string Name { get; }

        public int Score { get; }
    }
}
