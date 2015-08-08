using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using YouthClubApp.Models;
using YouthClubApp.Services;

namespace YouthClubApp.ViewModels
{
    public class TargetGameViewModel : ViewModelBase, IPageViewModel
    {
        private readonly DispatcherTimer animationTimer;
        private readonly IDictionary<Key, string> colours;
        private readonly DispatcherTimer gameTimer;
        private readonly IDictionary<Key, int> presses;
        private readonly Random rand;
        private readonly ISoundEffect audioPlayer;
        private int seconds = 30;
        private double vx;
        private double vy;
        private PlayerViewModel[] players;

        public event EventHandler Close;

        public TargetGameViewModel(PlayerViewModel[] players, ISoundEffect audioPlayer)
        {
            this.players = players;
            this.audioPlayer = audioPlayer;
            presses = players.ToDictionary(p => p.Key, p => 0);
            colours = players.ToDictionary(p => p.Key, p => p.Colour.ToString());
            rand = new Random();
            animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            animationTimer.Tick += DispatcherTimerOnTick;
            animationTimer.Start();
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            gameTimer.Tick += GameTimerOnTick;
            gameTimer.Start();
        }

        public CrossHairViewModel CrossHair { get; } = new CrossHairViewModel { X = 50, Y = 50 };

        public string Name
        {
            get
            {
                return "Bad Shooter";
            }
        }

        public int Seconds
        {
            get
            {
                return seconds;
            }

            set
            {
                if (seconds != value)
                {
                    seconds = value;
                    OnPropertyChanged(nameof(Seconds));
                }
            }
        }

        public ObservableCollection<ShotViewModel> Shots { get; } = new ObservableCollection<ShotViewModel>();

        public void OnShot(Key key, HitTypes hitType)
        {
            if (!presses.ContainsKey(key) || presses[key]++ >= 5)
            {
                return;
            }

            audioPlayer.Play();
            if (hitType == HitTypes.Miss)
            {
                return;
            }

            var shot = new ShotViewModel { X = CrossHair.X, Y = CrossHair.Y, Colour = colours[key] };
            Shots.Add(shot);
            vy = -2;
        }

        private void DispatcherTimerOnTick(object sender, EventArgs e)
        {
            var fx = 0.1 * (rand.NextDouble() - 0.5);
            var fy = 0.1 * (rand.NextDouble() - 0.5);
            CrossHair.X = Math.Max(Math.Min(CrossHair.X + vx, 100), 0);
            CrossHair.Y = Math.Max(Math.Min(CrossHair.Y + vy, 100), 0);
            vx += fx;
            vy += fy;
            vx = CrossHair.X == 0 ? 0.5 : vx;
            vy = CrossHair.Y == 0 ? 0.5 : vy;
            vx = CrossHair.X == 100 ? -0.5 : vx;
            vy = CrossHair.Y == 100 ? -0.5 : vy;
        }

        private void GameTimerOnTick(object sender, EventArgs e)
        {
            if (seconds <= 0)
            {
                OnClose();
            }

            Seconds--;
        }

        private void OnClose()
        {
            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}
