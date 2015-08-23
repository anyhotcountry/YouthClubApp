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
    public class BadShooterViewModel : CountdownViewModel, IPageViewModel
    {
        private static IList<string> countdowns = new[]
        {
            string.Empty,
            "Get Ready!",
            "3",
            "2",
            "1",
            "GO!"
        };

        private readonly DispatcherTimer animationTimer;
        private readonly ISoundEffect audioPlayer;
        private readonly DispatcherTimer gameTimer;
        private readonly IGunAimPhysics gunAimPhysics;
        private readonly Dictionary<Key, int> scores;
        private int count = 0;
        private string countDownText = countdowns[0];
        private PlayerViewModel[] players;
        private IDictionary<Key, int> presses;
        private int seconds = 30;

        public BadShooterViewModel(string name, double radius, PlayerViewModel[] players, ISoundEffect audioPlayer, IGunAimPhysics gunAimPhysics)
        {
            Name = name;
            Radius = radius;
            this.players = players;
            this.audioPlayer = audioPlayer;
            this.gunAimPhysics = gunAimPhysics;
            scores = players.ToDictionary(p => p.Key, p => 0);
            animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            animationTimer.Tick += DispatcherTimerOnTick;
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            gameTimer.Tick += CountDown;
        }

        public event EventHandler Close;

        public string CountDownText
        {
            get
            {
                return countDownText;
            }

            set
            {
                if (countDownText != value)
                {
                    countDownText = value;
                    OnPropertyChanged(nameof(CountDownText));
                }
            }
        }

        public CrossHairViewModel CrossHair { get; } = new CrossHairViewModel { X = 50, Y = 100 };

        public string Name { get; }

        public double Radius { get; }

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
            if (presses == null || !presses.ContainsKey(key) || presses[key]-- <= 0)
            {
                return;
            }

            audioPlayer.Play();
            gunAimPhysics.Jerk(0, -0.5);
            if (hitType == HitTypes.Miss)
            {
                return;
            }

            scores[key] += gunAimPhysics.GetScore(hitType);
            var shot = new ShotViewModel { X = CrossHair.X, Y = CrossHair.Y };
            Shots.Add(shot);
        }

        public void Start()
        {
            gameTimer.Start();
        }

        private void CountDown(object sender, EventArgs e)
        {
            count++;
            if (count < countdowns.Count)
            {
                CountDownText = countdowns[count];
            }
            else
            {
                gameTimer.Tick -= CountDown;
                gameTimer.Tick += GameTimerOnTick;
                CountDownText = string.Empty;
                presses = players.ToDictionary(p => p.Key, p => p.AllowedShots);
                animationTimer.Start();
            }
        }

        private void DispatcherTimerOnTick(object sender, EventArgs e)
        {
            CrossHair.X = gunAimPhysics.NextX(CrossHair.X);
            CrossHair.Y = gunAimPhysics.NextY(CrossHair.Y);
        }

        private void GameTimerOnTick(object sender, EventArgs e)
        {
            if (seconds <= 0)
            {
                gameTimer.Stop();
                animationTimer.Stop();
                OnClose();
            }

            Seconds--;
        }

        private void OnClose()
        {
            foreach (var player in players)
            {
                player.SetScore(Name, scores[player.Key]);
            }

            Close?.Invoke(this, EventArgs.Empty);
        }
    }
}