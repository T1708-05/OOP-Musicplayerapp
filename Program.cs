//17/11/2024 VŨ VĂN THÔNG xong đồ án
using System.Text;
using MusicPlayerApp.Models;
using MusicPlayerApp.Managers;

namespace MusicPlayerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;  //tao font chu cho dep

            // hien thi chao mung
            DisplayWelcomeScreen();

            AccountManager accountManager = new AccountManager();
            accountManager.AccountTest(); 

            Account currentAccount = null;


            bool isRunning = true;
            while (isRunning)
            {
                DisplayLoginMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        currentAccount = accountManager.UserLogin();
                        if (currentAccount != null)
                        {
                            // Khởi tạo PlaylistManager với thông tin người dùng và accountManager
                            PlaylistManager playlistManager = new PlaylistManager(currentAccount.UserInfo, accountManager);
                            MusicPlayerMenu(currentAccount, playlistManager, accountManager);
                        }
                        break;
                    case "2":
                        accountManager.UserRegister();
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn từ 1 đến 3.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }
//lam mau cho dep hhehe
            static void DisplayWelcomeScreen()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║♪ CHÀO MỪNG ĐẾN VỚI TRÌNH PHÁT NHẠC ♫ ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Đang tải...");

                int total = 30; // do dai cua thanh khi chay
                for (int i = 0; i <= total; i++)
                {
                    int percent = (i * 100) / total;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\r[");
                    Console.Write(new string('■', i));
                    Console.Write(new string('-', total - i));
                    Console.Write($"] {percent}%");
                    Console.ResetColor();
                    Thread.Sleep(100);
                }

                Console.WriteLine();
                Console.WriteLine("Tải xong!");
                Thread.Sleep(1000);
            }



        static void DisplayLoginMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌────────────────────────────────────────");
            Console.WriteLine("│          🎵  HỆ THỐNG ĐĂNG NHẬP  🎵   ");
            Console.WriteLine("├────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("│ 1. 🔑 Đăng nhập                        ");
            Console.WriteLine("│ 2. 📝 Đăng ký                          ");
            Console.WriteLine("│ 3. ❌ Thoát                            ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("└────────────────────────────────────────");
            Console.ResetColor();
            Console.Write("Vui lòng chọn một tùy chọn (1-3): ");
        }
// tao ra để có thể hiển thị thông tin bao nhiu playlist
            static void CreateOrAddPlaylist(Account account, PlaylistManager playlistManager)
            {
                Console.WriteLine("Chọn playlist để thêm:");
                Console.WriteLine("1. Thêm playlist 'Sơn Tùng M-TP Collection'");
                Console.WriteLine("2. Thêm playlist 'Top Hits VN'");
                Console.WriteLine("3. Thêm playlist Bolero");
                Console.WriteLine("4. Tạo playlist mới");
                Console.Write("Vui lòng chọn một playlist (1-4): ");

                string input= Console.ReadLine();

                Playlist newPlaylist = null;

                switch (input)
                {
                    case "1":
                        newPlaylist = playlistManager.AddSonTungPlaylist();
                        break;
                    case "2":
                        newPlaylist = playlistManager.AddTopHitsVN();
                        break;
                    case "3":
                        newPlaylist = playlistManager.AddBoleroPlaylist();
                        break;
                    case "4":
                        newPlaylist = playlistManager.CreatePlaylist();
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        static void MusicPlayerMenu(Account account, PlaylistManager playlistManager, AccountManager accountManager)
        {
            MusicPlayer player = new MusicPlayer();
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════════════════");
                Console.WriteLine($"║   🎧 TRÌNH PHÁT NHẠC - Xin chào {account.UserInfo.Name}");
                Console.WriteLine("╠══════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("║ 1. 📁 Tạo hoặc thêm playlist mới                  ");
                Console.WriteLine("║ 2. ➕ Thêm bài hát vào playlist                  ");
                Console.WriteLine("║ 3. 📜 Xem danh sách playlist và bài hát          ");
                Console.WriteLine("║ 4. ▶️  Phát playlist                             ");
                Console.WriteLine("║ 5. 🛠️ Quản lý playlist và thêm bài hát yêu thích❤️");
                Console.WriteLine("║ 6. ℹ️  Thông tin tài khoản                         ");
                Console.WriteLine("║ 7. ❤️ Phát danh sách bài hát yêu thích            ");
                Console.WriteLine("║ 8. 🔒  Đổi mật khâu                                ");
                Console.WriteLine("║ 9. 🔙 Đăng xuất                                   ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╚═══════════════════════════════════════════════════");
                Console.Write("Vui lòng chọn một tùy chọn (1-9): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateOrAddPlaylist(account, playlistManager);
                        break;
                    case "2":
                        
                        playlistManager.AddSongToPlaylist();
                        break;
                    case "3":
                        playlistManager.DisplayInfoSongs();
                        break;
                        // playlistManager.ViewPlaylists();
                    case "4":
                        PlayPlaylist(playlistManager, player);
                        break;
                    case "5":
                        ManagePlaylist(playlistManager, account,accountManager);
                        break;
                    case "6":
                        account.UserInfo.DisplayInfoUser();
                        break;
                    case "7":
                        account.UserInfo.PlayFavoriteSongs(player);
                        break;
                    case "8":
                        accountManager.ChangePassword(account);
                        break;
                    case "9":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        static void PlayPlaylist(PlaylistManager playlistManager, MusicPlayer player)
        {
            if (playlistManager.Playlists.Count == 0)
            {
                Console.WriteLine("Chưa có playlist nào.");
                return;
            }

            Console.WriteLine("Chọn playlist để phát:");
            for (int i = 0; i < playlistManager.Playlists.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {playlistManager.Playlists[i].Name}");
            }
            Console.Write("Nhập số thứ tự của playlist: ");
            if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlistManager.Playlists.Count)
            {
                Console.WriteLine("Playlist không hợp lệ.");
                return;
            }
            playlistIndex--;

            Playlist selectedPlaylist = playlistManager.Playlists[playlistIndex];

            if (selectedPlaylist.Songs.Count == 0)
            {
                Console.WriteLine("Playlist này không có bài hát nào.");
                return;
            }

            player.Playlist.Clear();
            foreach (var song in selectedPlaylist.Songs)
            {
                player.AddToPlayList(song);
            }

            bool isPlaying = true;
            while (isPlaying)
            {
                Song currentSong = selectedPlaylist.Songs[player.CurrentMediaIndex];

                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════");
                Console.WriteLine($"║   🎵 Đang phát playlist: {selectedPlaylist.Name.PadRight(25)} ");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════"); 
                Console.WriteLine($"║   🎧 Bài hát: {currentSong.Title.PadRight(34)}              ");
                Console.WriteLine($"║   👤 Nghệ sĩ: {currentSong.Artist.PadRight(33)}             ");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("║  1. 📃 Xem danh sách bài hát                    ");
                Console.WriteLine("║  2. 🎶 Chọn bài hát để phát                     ");
                Console.WriteLine("║  3. ⏸️  Tạm dừng nhạc                           ");
                Console.WriteLine("║  4. ▶️  Tiếp tục phát nhạc                      ");
                Console.WriteLine("║  5. ⏹️  Dừng nhạc                               ");
                Console.WriteLine("║  6. ⏭️  Bài hát tiếp theo                       ");
                Console.WriteLine("║  7. ⏮️  Bài hát trước                           ");
                Console.WriteLine("║  8. 📑 Hiển thị lời bài hát                     ");
                Console.WriteLine("║  9. 🔀 Phát ngẫu nhiên (Shuffle)                ");
                Console.WriteLine("║ 10. 🔁 Lặp lại bài hát hiện tại                 ");
                Console.WriteLine("║ 11. 🔙 Quay lại menu chính                      ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╚══════════════════════════════════════════════════════════════");
                Console.ResetColor();

                Console.Write("Vui lòng chọn một tùy chọn (1-11): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        playlistManager.ViewSongsInPlaylist(selectedPlaylist);
                        break;
                    case "2":
                        playlistManager.SelectAndPlaySong(selectedPlaylist, player);
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
                        player.ShowLyrics();
                        break;
                    case "9":
                        player.Shuffle();
                        break;
                    case "10":
                        player.Repeat();
                        break;
                    case "11":
                        isPlaying = false;
                        player.Stop();
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn từ 1 đến 12.");
                        break;
                }

                if (isPlaying)
                {
                    Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        static void ManagePlaylist(PlaylistManager playlistManager, Account account,AccountManager accountManager)
        {
            if (playlistManager.Playlists.Count == 0)
            {
                Console.WriteLine("Chưa có playlist nào.");
                return;
            }

            Console.WriteLine("Chọn playlist để quản lý:");
            for (int i = 0; i < playlistManager.Playlists.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {playlistManager.Playlists[i].Name}");
            }
                Console.Write("Nhập số thứ tự của playlist: ");
                if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > playlistManager.Playlists.Count)
                {
                    Console.WriteLine("Playlist không hợp lệ.");
                    return;
                }
        playlistIndex--;

        Playlist selectedPlaylist = playlistManager.Playlists[playlistIndex];

        bool isManaging = true;
        while (isManaging)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌──────────────────────────────────────────────────");
            Console.WriteLine($"│   🎶 Quản lý playlist: {selectedPlaylist.Name.PadRight(18)} ");
            Console.WriteLine("├──────────────────────────────────────────────────");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("│ 1. 📋 Xem danh sách bài hát              ");
            Console.WriteLine("│ 2. ❌ Xóa bài hát                        ");
            Console.WriteLine("│ 3. ✏️  Đổi tên playlist                  ");
            Console.WriteLine("│ 4. 🗑️  Xóa playlist                      ");
            Console.WriteLine("│ 5. ℹ️  Xem thông tin chi tiết bài hát    ");
            Console.WriteLine("│ 6. ❤️ Thêm bài hát vào danh sách yêu thích    ");
            Console.WriteLine("│ 7. 🔙 Quay lại                           ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("└──────────────────────────────────────────────────");
            Console.ResetColor();
            Console.Write("Vui lòng chọn một tùy chọn (1-7): ");

            string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("┌──────────────────────────────────────────┐");
                Console.WriteLine($"│     Danh sách bài hát trong playlist:   │");
                Console.WriteLine($"│        {selectedPlaylist.Name}          │");
                Console.WriteLine("├──────────────────────────────────────────┤");
                playlistManager.ViewSongsInPlaylist(selectedPlaylist);
                Console.WriteLine("└──────────────────────────────────────────┘");
                break;
            case "2":
                playlistManager.RemoveSongFromPlaylist(selectedPlaylist);
                break;
            case "3":
                playlistManager.RenamePlaylist(selectedPlaylist);
                break;
            case "4":
                Console.Write("Bạn có chắc chắn muốn xóa playlist này? (y/n): ");
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "y")
                {
                    playlistManager.RemovePlaylist();
                    isManaging = false;
                }
                break;
            case "5":
                playlistManager.ViewSongsInPlaylist(selectedPlaylist);
                playlistManager.ViewSongDetails(selectedPlaylist);
                break;
            case "6":
                Console.WriteLine("Chọn bài hát để thêm vào danh sách yêu thích:");
                playlistManager.ViewSongsInPlaylist(selectedPlaylist);
                Console.Write("Nhập số thứ tự bài hát: ");
                if (int.TryParse(Console.ReadLine(), out int songIndex) && songIndex > 0 && songIndex <= selectedPlaylist.Songs.Count)
                {
                    var currentSong = selectedPlaylist.Songs[songIndex - 1];
                    account.UserInfo.AddSongToFavorites(currentSong, accountManager);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("⚠️  Số thứ tự không hợp lệ.");
                    Console.ResetColor();
                }
                break;

            case "7":
                isManaging = false;
                break;
            default:
                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn từ 1 đến 5.");
                break;
        }

        if (isManaging)
        {
            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }


    }
        }
    }
}