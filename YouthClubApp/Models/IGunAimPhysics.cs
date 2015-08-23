namespace YouthClubApp.Models
{
    public interface IGunAimPhysics
    {
        int GetScore(HitTypes hitType);

        void Jerk(double x, double y);

        double NextX(double x);

        double NextY(double y);
    }
}