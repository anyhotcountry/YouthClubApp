using System;

namespace YouthClubApp.Models
{
    public class GunAimPhysics : IGunAimPhysics
    {
        private readonly Random rand;
        private double vx;
        private double vy;

        public GunAimPhysics()
        {
            rand = new Random();
            SpringConstant = 0.001;
            DampingConstant = 0.005;
            RandomConstant = 0.1;
        }

        public double SpringConstant { get; set; }

        public double DampingConstant { get; set; }

        public double RandomConstant { get; set; }

        public int GetScore(HitTypes hitType)
        {
            switch (hitType)
            {
                case HitTypes.Outer:
                    return 10;

                case HitTypes.Inner:
                    return 25;

                case HitTypes.BullsEye:
                    return 50;

                default:
                    return 0;
            }
        }

        public void Jerk(double x, double y)
        {
            vx += x;
            vy += y;
        }

        public double NextX(double x)
        {
            var fx = -SpringConstant * (x - 50) - DampingConstant * vx + RandomConstant * (rand.NextDouble() - 0.5);
            vx += fx;
            vx = x == 0 ? 0.5 : vx;
            vx = x == 100 ? -0.5 : vx;
            return Math.Min(Math.Max(0, vx + x), 100);
        }

        public double NextY(double y)
        {
            var fy = -SpringConstant * (y - 50) - DampingConstant * vy + RandomConstant * (rand.NextDouble() - 0.5);
            vy += fy;
            vy = y == 0 ? 0.5 : vy;
            vy = y == 100 ? -0.5 : vy;
            return Math.Min(Math.Max(0, vy + y), 100);
        }
    }
}