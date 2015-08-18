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
        private readonly DispatcherTimer animationTimer;
        private readonly DispatcherTimer gameTimer;
        private readonly IDictionary<Key, int> presses;
        private readonly ISoundEffect audioPlayer;
        private readonly IGunAimPhysics gunAimPhysics;
        private readonly Dictionary<Key, int> scores;
        private int seconds = 30;
        private PlayerViewModel[] players;

        public event EventHandler Close;

        public BadShooterViewModel(string name, double radius, PlayerViewModel[] players, ISoundEffect audioPlayer, IGunAimPhysics gunAimPhysics)
        {
            Name = name;
            Radius = radius;
            this.players = players;
            this.audioPlayer = audioPlayer;
            this.gunAimPhysics = gunAimPhysics;
            presses = players.ToDictionary(p => p.Key, p => 0);
            scores = players.ToDictionary(p => p.Key, p => 0);
            animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            animationTimer.Tick += DispatcherTimerOnTick;
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            gameTimer.Tick += GameTimerOnTick;
        }

        public void Start()
        {
            animationTimer.Start();
            gameTimer.Start();
        }

        public CrossHairViewModel CrossHair { get; } = new CrossHairViewModel { X = 50, Y = 100 };

        public string Name { get; }

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

        public double Radius { get; }

        public void OnShot(Key key, HitTypes hitType)
        {
            if (!presses.ContainsKey(key) || presses[key]++ >= 5)
            {
                return;
            }

            audioPlayer.Play();
            gunAimPhysics.Jerk(0, -2);
            if (hitType == HitTypes.Miss)
            {
                return;
            }

            scores[key] += gunAimPhysics.GetScore(hitType);
            var shot = new ShotViewModel { X = CrossHair.X, Y = CrossHair.Y };
            Shots.Add(shot);
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