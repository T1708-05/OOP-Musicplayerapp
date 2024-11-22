using MusicPlayerApp.Interfaces;

namespace MusicPlayerApp.Models
{
    public abstract class Media : IPlayable, IPlaylistControllable
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string FilePath { get; set; }

        public virtual void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Thong tin chi tiet :");
            Console.ResetColor();
            Console.WriteLine($"- Tieu de : {Title}");
            Console.WriteLine($"- The loai: {Genre}");
            Console.WriteLine($"- Ngay phat hanh : {ReleaseDate.ToShortDateString()}");
            Console.WriteLine($"- Duong dan tep : {FilePath}");
        }

        public abstract void Play();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
        public virtual void Next() { Console.WriteLine("Chuyen sang bai tiep theo..."); }
        public virtual void Previous() { Console.WriteLine("Quay lai bai truoc..."); }
        public virtual void Shuffle() { Console.WriteLine(" playlist..."); }
        public virtual void Repeat() { Console.WriteLine("Lap lai bai hat..."); }
    }
}
