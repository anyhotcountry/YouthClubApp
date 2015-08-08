using System.IO;
using System.Media;

namespace YouthClubApp.Services
{
    public class SoundEffect : ISoundEffect
    {
        private readonly SoundPlayer player = new SoundPlayer();

        public SoundEffect(string fileName)
        {
            player.Stream = File.OpenRead(fileName);
            player.Load();
        }

        public void Play()
        {
            player.Play();
        }
    }
}
