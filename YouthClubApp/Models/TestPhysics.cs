namespace YouthClubApp.Models
{
    public class TestPhysics : IGunAimPhysics
    {
        private double x;
        private double y;

        public TestPhysics(double x, double y)
        {
            this.x = x;
            this.y = y;
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
            return this.x;
        }

        public double NextY(double y)
        {
            return this.y;
        }
    }
}
