
using MusicPlayerApp.Interfaces;

namespace MusicPlayerApp.Models
{
    public class MusicPlayer : IPlayable, IPlaylistControllable
    {
        private IPlayable currentMedia;
        private int currentIndex = 0;

        public int CurrentMediaIndex
        {
            get { return currentIndex; }
            set
            {
                if (value >= 0 && value < Playlist.Count)
                {
                    currentIndex = value;
                    currentMedia = Playlist[currentIndex];
                }
                else
                {
                    Console.WriteLine("Chỉ số bài hát không hợp lệ.");
                }
            }
        }

        public List<IPlayable> Playlist { get; set; } = new List<IPlayable>();

        public void Play()
        {
            if (Playlist.Count > 0)
            {
                currentMedia = Playlist[currentIndex];
                currentMedia.Play();
            }
            else
            {
                Console.WriteLine("Playlist trống.");
            }
        }

        public void Pause()
        {
            currentMedia?.Pause();
        }

        public void Resume()
        {
            if (currentMedia != null)
            {
                currentMedia.Resume();
            }
            else
            {
                Console.WriteLine("Không có bài hát nào đang phát.");
            }
        }

        public void Stop()
        {
            currentMedia?.Stop();
            currentMedia = null;
        }

        public void Next()
        {
            if (Playlist.Count > 0)
            {
                currentMedia?.Stop();
                currentIndex = (currentIndex + 1) % Playlist.Count;
                Play();
            }
            else
            {
                Console.WriteLine("Playlist trống.");
            }
        }

        public void Previous()
        {
            if (Playlist.Count > 0)
            {
                currentMedia?.Stop();
                currentIndex = (currentIndex - 1 + Playlist.Count) % Playlist.Count;
                Play();
            }
            else
            {
                Console.WriteLine("Playlist trống.");
            }
        }
        public void Shuffle()
        {
            if (Playlist.Count > 0)
            {
                currentMedia?.Stop();
                Random rnd = new Random();
                currentIndex = rnd.Next(Playlist.Count);
                Play();
            }
            else
            {
                Console.WriteLine("Playlist trống.");
            }
        }

        public void Repeat()
        {
            if (currentMedia != null)
            {
                currentMedia.Stop();
                Play();
            }
            else
            {
                Console.WriteLine("Không có bài hát nào đang phát.");
            }
        }

        public void AddToPlayList(IPlayable media)
        {
            Playlist.Add(media);
        }

        public void RemoveFromPlayList(IPlayable media)
        {
            Playlist.Remove(media);
        }

        public void ShowLyrics()
        {
            if (currentMedia is Song song)
            {
                song.ShowLyrics();
            }
            else
            {
                Console.WriteLine("Không thể hiển thị lời cho phương tiện này.");
            }
        }

        // Phương thức để chọn và phát bài hát theo chỉ số
        public void PlaySongAtIndex(int index)
        {
            if (index >= 0 && index < Playlist.Count)
            {
                currentMedia?.Stop();
                currentIndex = index;
                Play();
            }
            else
            {
                Console.WriteLine("Chỉ số bài hát không hợp lệ.");
            }
        }
        public Song GetCurrentSong()
        {
            if (Playlist.Count > 0 && CurrentMediaIndex >= 0 && CurrentMediaIndex < Playlist.Count)
            {
                if (Playlist[CurrentMediaIndex] is Song song)
                {
                    return song;
                }
                else
                {
                    Console.WriteLine("Phương tiện hiện tại không phải là bài hát.");
                    return null;
                }
            }
            return null;
        }


    }
}
