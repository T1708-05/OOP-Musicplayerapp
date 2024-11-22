namespace MusicPlayerApp.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();

        public void AddSong(Song song) { Songs.Add(song); }
        public void RemoveSong(Song song) { Songs.Remove(song); }
        public void RenamePlaylist(string newName) { Name = newName; }
        public virtual void DisplayInfoSongs()
        {
            Console.WriteLine($"Playlist: {Name}, Số bài hát: {Songs.Count}");
            foreach (var song in Songs)
            {
                Console.WriteLine($"- {song.Title} by {song.Artist}");
            }
        }
    }
}
