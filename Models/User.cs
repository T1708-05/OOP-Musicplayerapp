using MusicPlayerApp.Managers;
namespace MusicPlayerApp.Models
{
    public class User : Person
    {
        public List<Playlist> MyPlaylists { get; set; } = new List<Playlist>();
        public List<Song> FavoriteSongs { get; set; } = new List<Song>();

        // Thêm playlist vào danh sách của người dùng
        public void AddPlaylist(Playlist playlist)
        {
            MyPlaylists.Add(playlist);
        }

        // Thêm bài hát vào danh sách yêu thích
        public void AddFavoriteSong(Song song)
        {
            if (song != null)
            {
                // Kiểm tra xem bài hát đã tồn tại trong danh sách yêu thích hay chưa
                if (FavoriteSongs.Any(s => s.Title == song.Title && s.Artist == song.Artist))    // tai vi them bai hat yeu thich se bi trung 2 lan
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"⚠️  Bài hát '{song.Title}' đã có trong danh sách yêu thích.");
                    Console.ResetColor();
                    return;
                }

                // Thêm bài hát nếu chưa tồn tại
                FavoriteSongs.Add(song);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Đã thêm bài hát '{song.Title}' vào danh sách yêu thích.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Không thể thêm bài hát vì thông tin không hợp lệ.");
                Console.ResetColor();
            }
        }
        public void AddSongToFavorites(Song song, AccountManager accountManager)
        {
            if (song != null)
            {
                // Kiểm tra xem bài hát đã tồn tại trong danh sách yêu thích hay chưa
                if (FavoriteSongs.Any(s => s.Title == song.Title && s.Artist == song.Artist))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"⚠️  Bài hát '{song.Title}' đã có trong danh sách yêu thích.");
                    Console.ResetColor();
                    return;
                }

                // Thêm bài hát nếu chưa tồn tại
                FavoriteSongs.Add(song);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✅ Đã thêm bài hát '{song.Title}' vào danh sách yêu thích.");
                Console.ResetColor();

                // Lưu lại thông tin tài khoản
                accountManager.SaveAccounts();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Không thể thêm bài hát vì thông tin không hợp lệ.");
                Console.ResetColor();
            }
        }



        // Xem danh sách playlist của người dùng
        public void ViewMyPlaylists()
        {
            if (MyPlaylists.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Bạn chưa có playlist nào.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("🎵 Danh sách playlist của bạn:");
                Console.ResetColor();
                foreach (var playlist in MyPlaylists)
                {
                    Console.WriteLine($"- {playlist.Name}");
                }
            }
        }

        // Xem danh sách bài hát yêu thích
        public void ViewFavoriteSongs()
        {
            if (FavoriteSongs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Bạn chưa có bài hát yêu thích nào.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("❤️ Danh sách bài hát yêu thích của bạn:");
                Console.ResetColor();
                foreach (var song in FavoriteSongs)
                {
                    Console.WriteLine($"- {song.Title} - {song.Artist}");
                }
            }
        }
        public void PlayFavoriteSongs( MusicPlayer player)
        {
            if (FavoriteSongs.Count == 0)
            {
                Console.WriteLine("⚠️  Không có bài hát yêu thích nào để phát.");
                return;
            }

            player.Playlist.Clear();
            foreach (var song in FavoriteSongs)
            {
                player.AddToPlayList(song);
            }

            bool isPlaying = true;
            while (isPlaying)
            {
                Song currentSong = FavoriteSongs[player.CurrentMediaIndex];

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════");
                Console.WriteLine("║   ❤️ Đang phát từ danh sách bài hát yêu thích                ");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════");
                Console.WriteLine($"║   🎧 Bài hát: {currentSong.Title.PadRight(34)}              ");
                Console.WriteLine($"║   👤 Nghệ sĩ: {currentSong.Artist.PadRight(33)}             ");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════");
                Console.ResetColor();

                Console.WriteLine("║ 1. 📃 Hiển thị danh sách bài hát yêu thích");
                Console.WriteLine("║ 2. 🎶 Chọn bài hát để phát");
                Console.WriteLine("║ 3. ⏸️  Tạm dừng nhạc");
                Console.WriteLine("║ 4. ▶️  Tiếp tục phát nhạc");
                Console.WriteLine("║ 5. ⏹️  Dừng nhạc");
                Console.WriteLine("║ 6. ⏭️  Bài hát tiếp theo");
                Console.WriteLine("║ 7. ⏮️  Bài hát trước");
                Console.WriteLine("║ 8. 🔀 Phát ngẫu nhiên (Shuffle)");
                Console.WriteLine("║ 9. 🔙 Quay lại menu chính");
                Console.ResetColor();
                Console.Write("Vui lòng chọn một tùy chọn (1-9): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("❤️ Danh sách bài hát yêu thích:");
                        Console.ResetColor();
                        for (int i = 0; i < FavoriteSongs.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {FavoriteSongs[i].Title} - {FavoriteSongs[i].Artist}");
                        }
                        Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("❤️ Chọn bài hát yêu thích để phát:");
                        Console.ResetColor();
                        for (int i = 0; i < FavoriteSongs.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {FavoriteSongs[i].Title} - {FavoriteSongs[i].Artist}");
                        }
                        Console.Write("Nhập số thứ tự bài hát: ");
                        if (int.TryParse(Console.ReadLine(), out int songIndex) &&
                            songIndex > 0 && songIndex <= FavoriteSongs.Count)
                        {
                            player.CurrentMediaIndex = songIndex - 1; // Đặt bài hát được chọn làm bài hiện tại
                            player.Play(); // Bắt đầu phát bài hát
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"▶️ Đang phát: {FavoriteSongs[songIndex - 1].Title} - {FavoriteSongs[songIndex - 1].Artist}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("⚠️  Số thứ tự không hợp lệ.");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                        Console.ReadKey();
                        break;

                    case "3":
                        player.Pause();
                        break;

                    case "4":
                        player.Resume();
                        break;

                    case "5":
                        player.Stop();
                        isPlaying = false;
                        break;

                    case "6":
                        player.Next();
                        break;

                    case "7":
                        player.Previous();
                        break;

                    case "8":
                        player.Shuffle();
                        break;

                    case "9":
                        isPlaying = false;
                        player.Stop();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("⚠️  Lựa chọn không hợp lệ.");
                        Console.ResetColor();
                        break;
                }

                if (isPlaying)
                {
                    Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }


        // Hiển thị thông tin người dùng
        public override void DisplayInfoUser()
        {
            base.DisplayInfoUser();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🎵 Tổng số playlist: {MyPlaylists.Count}");
            Console.WriteLine($"❤️ Tổng số bài hát yêu thích: {FavoriteSongs.Count}");
            Console.ResetColor();
            ViewMyPlaylists();
            ViewFavoriteSongs();
        }
    }
}
