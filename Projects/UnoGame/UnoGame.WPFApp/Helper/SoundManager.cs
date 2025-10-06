using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UnoGame.WPFApp.Helper
{
    internal class SoundManager
    {
        private static MediaPlayer _bgmPlayer;
        private static bool _isMuted = false;

        public static bool IsMuted
        {
            get => _isMuted;
            set
            {
                _isMuted = value;
                if (_bgmPlayer != null)
                    _bgmPlayer.IsMuted = true;
            }
        }

        public static void PlaySound(string soundFileName)
        {
            try
            {
                var uri = $"pack://application:,,,/Resources/Sound/{soundFileName}.wav";
                using var stream = App.GetResourceStream(new Uri(uri))?.Stream;
                if (stream != null)
                {
                    var player = new SoundPlayer(stream);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Sound Manager] Error playing sound {soundFileName}: {ex.Message}");
            }
        }

        public static void PlayBacksound(string soundFileName)
        {
            _bgmPlayer ??= new MediaPlayer();

            try
            {
                _bgmPlayer.Open(new Uri($"pack://application:,,,/Resources/Sound/{soundFileName}.mp3"));
                _bgmPlayer.Volume = 1;
                _bgmPlayer.MediaEnded += (s, e) =>
                {
                    _bgmPlayer.Position = TimeSpan.Zero;
                    _bgmPlayer.Play();
                };

                if (!_isMuted)
                {
                    _bgmPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing BGM {soundFileName}: {ex.Message}");
            }
        }

        public static void StopAllPlayer()
        {
            var player = new SoundPlayer();
            player.Stop();
        }

        public static void StopBacksound()
        {
            _bgmPlayer.Stop();
        }
    }
}
