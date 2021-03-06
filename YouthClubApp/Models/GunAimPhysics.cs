﻿using System;

namespace YouthClubApp.Models
{
    public class GunAimPhysics : IGunAimPhysics
    {
        private const double Tolerance = 0;
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

        public double DampingConstant { get; set; }

        public double RandomConstant { get; set; }

        public double SpringConstant { get; set; }

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
            var fx = (-SpringConstant * (x - 50)) - (DampingConstant * vx * Math.Abs(vx)) + (RandomConstant * (rand.NextDouble() - 0.5));
            vx += fx;
            vx = Math.Abs(x) < Tolerance ? 0.5 : vx;
            vx = Math.Abs(x - 100) < Tolerance ? -0.5 : vx;
            return Math.Min(Math.Max(0, vx + x), 100);
        }

        public double NextY(double y)
        {
            var fy = (-SpringConstant * (y - 50)) - (DampingConstant * vy * Math.Abs(vy)) + (RandomConstant * (rand.NextDouble() - 0.5));
            vy += fy;
            vy = Math.Abs(y) < Tolerance ? 0.5 : vy;
            vy = Math.Abs(y - 100) < Tolerance ? -0.5 : vy;
            return Math.Min(Math.Max(0, vy + y), 100);
        }
    }
}