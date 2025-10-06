using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UnoGame.WPFApp.Helper
{
    internal class SoundManager
    {
        private static MediaPlayer? _bgmPlayer;
        private static bool _isMuted = false;

        public static bool IsMuted
        {
            get => _isMuted;
            set
            {
                _isMuted = value;
                _bgmPlayer?.IsMuted = value;
            }
        }

        public static void PlaySound(string soundFileName)
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", $"{soundFileName}.wav");
                if (File.Exists(path))
                {
                    Debug.WriteLine(path);
                    var player = new SoundPlayer(path);
                    player.Play();
                }
                else
                {
                    Debug.WriteLine($"[Sound Manager] File not found: {path}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Sound Manager] Error playing sound {soundFileName}: {ex.Message}");
            }
        }

        public static void PlayBacksound()
        {
            _bgmPlayer ??= new MediaPlayer();
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sound", "bgm.mp3");
                _bgmPlayer.Open(new Uri(path));
                _bgmPlayer.Volume = 0.3;
                _bgmPlayer.MediaEnded += new EventHandler(MediaPlayerEnded);

                if (!_isMuted)
                {
                    _bgmPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error playing BGM: {ex.Message}");
            }
        }

        public static void StopAllPlayer()
        {
            var player = new SoundPlayer();
            player.Stop();
        }

        public static void StopBacksound()
        {
            _bgmPlayer?.Stop();
        }

        private static void MediaPlayerEnded(object? sender, EventArgs e)
        {
            _bgmPlayer.Position = TimeSpan.Zero;
            _bgmPlayer.Play();
        }
    }
}
