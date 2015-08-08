using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace YouthClubApp.ViewModels
{
    public class PlayerViewModel
    {
        private readonly Dictionary<string, int> scores;

        public PlayerViewModel(string name, Key key, Color colour)
        {
            Name = name;
            Key = key;
            Colour = colour;
            scores = new Dictionary<string, int>();
        }

        public Key Key { get; }

        public string Name { get; }
        public Color Colour { get; }

        public void SetScore(string game, int score)
        {
            scores[game] = score;
        }

        public int GetScore(string game)
        {
            int score;
            scores.TryGetValue(game, out score);
            return score;
        }

    }
}
