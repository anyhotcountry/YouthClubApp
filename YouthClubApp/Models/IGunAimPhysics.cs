namespace YouthClubApp.Models
{
    public interface IGunAimPhysics
    {
        double NextX(double x);

        double NextY(double y);

        void Jerk(double x, double y);

        int GetScore(HitTypes hitType);
    }
}