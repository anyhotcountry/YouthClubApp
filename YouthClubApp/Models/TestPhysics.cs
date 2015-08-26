namespace YouthClubApp.Models
{
    public class TestPhysics : IGunAimPhysics
    {
        private readonly double fixedX;
        private readonly double fixedY;

        public TestPhysics(double x, double y)
        {
            fixedX = x;
            fixedY = y;
        }

        public int GetScore(HitTypes hitType)
        {
            return 0;
        }

        public void Jerk(double x, double y)
        {
        }

        public double NextX(double x)
        {
            return fixedX;
        }

        public double NextY(double y)
        {
            return fixedY;
        }
    }
}