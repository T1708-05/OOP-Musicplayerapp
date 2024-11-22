using Newtonsoft.Json;
using NAudio.Wave;
using MusicPlayerApp.Interfaces;   // Cài đặt để phát nhạc: dotnet add package NAudio

namespace MusicPlayerApp.Models
{
    public class Song : Media,IPlayable
    {
        public string Artist { get; set; }
        public string LyricsPath { get; set; }

        [JsonIgnore] // Bỏ qua khi serialize
        private AudioFileReader audioFile;

        [JsonIgnore] // Bỏ qua khi serialize
        private WaveOutEvent outputDevice;

        public override void Play()   // phat bai hat
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"❌ Không tìm thấy tệp tin nhạc: {FilePath}");
                    Console.ResetColor();
                    return;
                }
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    audioFile = new AudioFileReader(FilePath);
                    outputDevice.Init(audioFile);
                }
                outputDevice.Play();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"▶️  Đang phát bài hát: '{Title}' của {Artist}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Lỗi khi phát bài hát: {ex.Message}");
                Console.ResetColor();
            }
        }

        public override void Pause()
        {
            outputDevice?.Pause();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⏸️  Tạm dừng bài hát: '{Title}'");
            Console.ResetColor();
        }

        public override void Resume()
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Paused)
            {
                outputDevice.Play();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"▶️  Tiếp tục phát bài hát: '{Title}'");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Không có bài hát nào đang tạm dừng.");
                Console.ResetColor();
            }
        }

        public override void Stop()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
            outputDevice = null;
            audioFile?.Dispose();
            audioFile = null;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⏹️  Dừng phát bài hát: '{Title}'");
            Console.ResetColor();
        }

        public void ShowLyrics()
        {
            if (!string.IsNullOrEmpty(LyricsPath) && File.Exists(LyricsPath))  // doc file
            {
                string lyrics = File.ReadAllText(LyricsPath);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"📃 Lời bài hát '{Title}':\n");
                Console.ResetColor();
                Console.WriteLine(lyrics);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Không tìm thấy lời bài hát.");
                Console.ResetColor();
            }
        }
        public override void DisplayInfo()   // hien thi them ca si va loi bai hat ->> thong tin chi tet
        {
            base.DisplayInfo();
            Console.WriteLine($"- Nghệ sĩ: {Artist}");
            if (!string.IsNullOrEmpty(LyricsPath))
            {
                Console.WriteLine($"- Đường dẫn lời bài hát: {LyricsPath}");  
            }
        }
    }
}
