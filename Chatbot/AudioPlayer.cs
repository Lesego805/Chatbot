// Commit 3: MediaPlayer initialization and voice greeting playback handler
using System;
using System.IO;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    public static class AudioPlayer
    {
        private static MediaPlayer? _mediaPlayer;

        public static void PlayGreeting()
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");

                if (File.Exists(audioPath))
                {
                    // Ensure the MediaPlayer instance stays alive and open the file using a Uri object
                    _mediaPlayer = new MediaPlayer();
                    _mediaPlayer.Open(new Uri(audioPath, UriKind.Absolute));
                    _mediaPlayer.Play();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Audio Error] File not found at: {audioPath}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Audio Error] MediaPlayer failed: {ex.Message}");
            }
        }
    }
}