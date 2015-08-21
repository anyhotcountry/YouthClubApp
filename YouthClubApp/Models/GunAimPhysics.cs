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
        }

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
            var fx = -0.001 * (x - 50) + 0.1 * (rand.NextDouble() - 0.5);
            vx += fx;
            vx = x == 0 ? 0.5 : vx;
            vx = x == 100 ? -0.5 : vx;
            return Math.Min(Math.Max(0, vx + x), 100);
        }

        public double NextY(double y)
        {
            var fy = -0.001 * (y - 50) * rand.NextDouble() + 0.1 * (rand.NextDouble() - 0.5);
            vy += fy;
            vy = y == 0 ? 0.5 : vy;
            vy = y == 100 ? -0.5 : vy;
            return Math.Min(Math.Max(0, vy + y), 100);
        }
    }
}