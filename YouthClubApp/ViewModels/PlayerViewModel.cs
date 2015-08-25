using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace YouthClubApp.ViewModels
{
    public class PlayerViewModel
    {
        private readonly Dictionary<string, int> scores;

        public PlayerViewModel(string name, Key key, int allowedShots)
        {
            Name = name;
            Key = key;
            AllowedShots = allowedShots;
            scores = new Dictionary<string, int>();
        }

        public int AllowedShots { get; }

        public Key Key { get; }

        public string Name { get; }

        public int GetScore(string game)
        {
            int score;
            scores.TryGetValue(game, out score);
            return score;
        }

        public int GetScore()
        {
            var score = scores.Values.Sum();
            return score;
        }

        public void SetScore(string game, int score)
        {
            scores[game] = score;
        }
    }
}