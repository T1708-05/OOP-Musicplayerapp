using System;
using System.Collections.Generic;
using MusicPlayerApp.Models;

namespace MusicPlayerApp.Managers
{
    public class PlaylistManager : Playlist
    {
        private User currentUser;
        private AccountManager accountManager;

        public PlaylistManager(User user, AccountManager accManager)
        {
            currentUser = user;
            accountManager = accManager;
        }

        public List<Playlist> Playlists
        {
            get { return currentUser.MyPlaylists; }
        }

        // Thêm playlist mới
        public void AddPlaylist(Playlist playlist)
        {
            currentUser.MyPlaylists.Add(playlist);
            accountManager.SaveAccounts();
        }

        // Tạo một playlist mới
        public Playlist CreatePlaylist()
        {
            Console.Write("🎵 Nhập tên playlist mới: ");
            string playlistName = Console.ReadLine();
            Playlist newPlaylist = new Playlist { Name = playlistName };
            currentUser.AddPlaylist(newPlaylist);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Đã tạo playlist '{playlistName}' thành công.");
            Console.ResetColor();
            return newPlaylist;
        }

        // Xem danh sách các playlist
        // public void ViewPlaylists()
        // {
        //     if (currentUser.MyPlaylists.Count == 0)
        //     {
        //         Console.ForegroundColor = ConsoleColor.Yellow;
        //         Console.WriteLine("⚠️  Bạn chưa có playlist nào.");
        //         Console.ResetColor();
        //         return;
        //     }

        //     Console.ForegroundColor = ConsoleColor.Cyan;
        //     Console.WriteLine("🎵 Danh sách các playlist của bạn:");
        //     Console.ResetColor();
        //     for (int i = 0; i < currentUser.MyPlaylists.Count; i++)
        //     {
        //         Console.WriteLine($"{i + 1}. {currentUser.MyPlaylists[i].Name} - {currentUser.MyPlaylists[i].Songs.Count} bài hát");
        //     }
        // }

        // Xóa một playlist
        public void RemovePlaylist()
        {
            if (currentUser.MyPlaylists.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Bạn chưa có playlist nào.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🗑️  Chọn playlist để xóa:");
            Console.ResetColor();
            for (int i = 0; i < currentUser.MyPlaylists.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {currentUser.MyPlaylists[i].Name}");
            }
            Console.Write("🔢 Nhập số thứ tự của playlist: ");
            if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > currentUser.MyPlaylists.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Playlist không hợp lệ.");
                Console.ResetColor();
                return;
            }
            playlistIndex--;

            var playlistToRemove = currentUser.MyPlaylists[playlistIndex];
            currentUser.MyPlaylists.Remove(playlistToRemove);
            accountManager.SaveAccounts();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Đã xóa playlist '{playlistToRemove.Name}' khỏi danh sách.");
            Console.ResetColor();
        }

        // Đổi tên một playlist
        public void RenamePlaylist(Playlist selectedPlaylist)
        {
            Console.Write("✏️  Nhập tên mới cho playlist: ");
            string newName = Console.ReadLine();
            selectedPlaylist.RenamePlaylist(newName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Đã đổi tên playlist thành công.");
            Console.ResetColor();
        }

        // Thêm bài hát vào playlist
        public void AddSongToPlaylist()
        {
            if (currentUser.MyPlaylists.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Bạn chưa có playlist nào. Vui lòng tạo playlist trước.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🎵 Chọn playlist để thêm bài hát:");
            Console.ResetColor();
            for (int i = 0; i < currentUser.MyPlaylists.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {currentUser.MyPlaylists[i].Name}");
            }
            Console.Write("🔢 Nhập số thứ tự của playlist: ");
            if (!int.TryParse(Console.ReadLine(), out int playlistIndex) || playlistIndex < 1 || playlistIndex > currentUser.MyPlaylists.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Playlist không hợp lệ.");
                Console.ResetColor();
                return;
            }
            playlistIndex--;

            Playlist selectedPlaylist = currentUser.MyPlaylists[playlistIndex];

            Console.Write("🎶 Nhập tên bài hát: ");
            string title = Console.ReadLine();
            Console.Write("👤 Nhập tên nghệ sĩ: ");
            string artist = Console.ReadLine();
            Console.Write("🎼 Nhập thể loại: ");
            string genre = Console.ReadLine();
            Console.Write("📅 Nhập ngày phát hành (dd/MM/yyyy): ");
            DateTime releaseDate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out releaseDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("❌ Ngày phát hành không hợp lệ. Vui lòng nhập lại (dd/MM/yyyy): ");
                Console.ResetColor();
            }

            Console.Write("📁 Nhập đường dẫn tệp nhạc: ");
            string filePath = Console.ReadLine();
            Console.Write("📝 Nhập đường dẫn lời bài hát (có thể bỏ qua): ");
            string lyricsPath = Console.ReadLine();

            Song newSong = new Song
            {
                Title = title,
                Artist = artist,
                Genre = genre,
                ReleaseDate = releaseDate,
                FilePath = filePath,
                LyricsPath = lyricsPath
            };

            selectedPlaylist.AddSong(newSong);
            accountManager.SaveAccounts();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Đã thêm bài hát '{title}' vào playlist '{selectedPlaylist.Name}'.");
            Console.ResetColor();
        }

        // Xóa bài hát khỏi playlist
        public void RemoveSongFromPlaylist(Playlist selectedPlaylist)
        {
            if (selectedPlaylist.Songs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Playlist này không có bài hát nào.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🗑️  Chọn bài hát để xóa:");
            Console.ResetColor();
            for (int i = 0; i < selectedPlaylist.Songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedPlaylist.Songs[i].Title} - {selectedPlaylist.Songs[i].Artist}");
            }
            Console.Write("🔢 Nhập số thứ tự của bài hát: ");
            if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > selectedPlaylist.Songs.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Bài hát không hợp lệ.");
                Console.ResetColor();
                return;
            }
            songIndex--;
            var songToRemove = selectedPlaylist.Songs[songIndex];
            selectedPlaylist.RemoveSong(songToRemove);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Đã xóa bài hát '{songToRemove.Title}' khỏi playlist.");
            Console.ResetColor();
        }

        // Hiển thị danh sách bài hát trong playlist
        public void ViewSongsInPlaylist(Playlist selectedPlaylist)
        {
            if (selectedPlaylist.Songs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Playlist này không có bài hát nào.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"🎵 Danh sách bài hát trong playlist '{selectedPlaylist.Name}':");
            Console.ResetColor();
            for (int i = 0; i < selectedPlaylist.Songs.Count; i++)
            {
                Song song = selectedPlaylist.Songs[i];
                Console.WriteLine($"{i + 1}. 🎵 {song.Title} - 👤 {song.Artist}");
            }
        }

        // Chọn và phát bài hát từ playlist
        public void SelectAndPlaySong(Playlist selectedPlaylist, MusicPlayer player)
        {
            if (selectedPlaylist.Songs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Playlist này không có bài hát nào.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"🎵 Danh sách bài hát trong playlist '{selectedPlaylist.Name}':");
            Console.ResetColor();
            for (int i = 0; i < selectedPlaylist.Songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedPlaylist.Songs[i].Title} - {selectedPlaylist.Songs[i].Artist}");
            }

            Console.Write("▶️  Nhập số thứ tự của bài hát để phát: ");
            if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > selectedPlaylist.Songs.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Bài hát không hợp lệ.");
                Console.ResetColor();
                return;
            }
            songIndex--;
            player.CurrentMediaIndex = songIndex;
            player.Play();
        }

        // Xem thông tin chi tiết của bài hát
        public void ViewSongDetails(Playlist selectedPlaylist)
        {
            Console.Write("Nhập số thứ tự của bài hát để xem thông tin chi tiết: ");
            if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > selectedPlaylist.Songs.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Bài hát không hợp lệ.");
                Console.ResetColor();
                return;
            }
            songIndex--;
            Song selectedSong = selectedPlaylist.Songs[songIndex];
            selectedSong.DisplayInfo();
        }

        // Thêm các playlist mẫu nếu cần
        // ...
        // ==========================
        // Thêm playlist mẫu
        // ==========================

        // Thêm playlist Top Hits VN mẫu
        public Playlist AddTopHitsVN()
        {
            Playlist tophitPlaylist = new Playlist { Name = "Top Hits VN" };

            Song song1 = new Song
            {
                Title = "Waiting For You",
                Artist = "MONO",
                Genre = "Pop",
                ReleaseDate = new DateTime(2022, 8, 18),
                FilePath = @"C:\DOAN\topbaihitVN\waitingforyou.mp3",
            };

            Song song2 = new Song
            {
                Title = "Bên Trên Tầng Lầu",
                Artist = "Tăng Duy Tân",
                Genre = "Ballad",
                ReleaseDate = new DateTime(2022, 6, 11),
                FilePath = @"C:\DOAN\topbaihitVN\bentrentanglau.mp3",
            };

            Song song3 = new Song
            {
                Title = "Chìm Sâu",
                Artist = "MCK ft. Trung Trần",
                Genre = "Rap",
                ReleaseDate = new DateTime(2022, 9, 5),
                FilePath = @"C:\DOAN\topbaihitVN\chimsau.mp3",
            };

            Song song4 = new Song
            {
                Title = "Vì Mẹ Anh Bắt Chia Tay",
                Artist = "Miu Lê ft. Karik",
                Genre = "Pop",
                ReleaseDate = new DateTime(2022, 10, 21),
                FilePath = @"C:\DOAN\topbaihitVN\vimeanhbatchiatay.mp3",
            };

            Song song5 = new Song
            {
                Title = "Hãy Trao Cho Anh",
                Artist = "Sơn Tùng M-TP",
                Genre = "Pop",
                ReleaseDate = new DateTime(2019, 7, 1),
                FilePath = @"C:\DOAN\topbaihitVN\haytraochoanh.mp3",
            };

            tophitPlaylist.AddSong(song1);
            tophitPlaylist.AddSong(song2);
            tophitPlaylist.AddSong(song3);
            tophitPlaylist.AddSong(song4);
            tophitPlaylist.AddSong(song5);

            AddPlaylist(tophitPlaylist);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Đã thêm playlist 'Top Hits VN' với 5 bài hát.");
            Console.ResetColor();
            return tophitPlaylist;
        }

        // Thêm playlist Sơn Tùng M-TP Collection
        public Playlist AddSonTungPlaylist()
        {
            Playlist sonTungPlaylist = new Playlist { Name = "Sơn Tùng M-TP Collection" };

            Song song1 = new Song
            {
                Title = "Chúng Ta Của Tương Lai",
                Artist = "Sơn Tùng M-TP",
                ReleaseDate = new DateTime(2023, 1, 1),
                Genre = "Pop",
                FilePath = @"C:\DOAN\songsSonTung\chungtacuatuonglai.mp3",
                LyricsPath = @"C:\DOAN\lyricsSonTung\chungtacuatuonglai.txt"
            };

            Song song2 = new Song
            {
                Title = "Lạc Trôi",
                Artist = "Sơn Tùng M-TP",
                Genre = "Pop",
                ReleaseDate = new DateTime(2017, 1, 1),
                FilePath = @"C:\DOAN\songsSonTung\lactroi.mp3",
                LyricsPath = @"C:\DOAN\lyricsSonTung\lactroi.txt"
            };

            Song song3 = new Song
            {
                Title = "Đừng Làm Trái Tim Anh Đau",
                Artist = "Sơn Tùng M-TP",
                Genre = "Pop",
                ReleaseDate = new DateTime(2018, 5, 5),
                FilePath = @"C:\DOAN\songsSonTung\dunglamtraitimanhdau.mp3",
                LyricsPath = @"C:\DOAN\lyricsSonTung\dunglamtraitimanhdau.txt"
            };

            Song song4 = new Song
            {
                Title = "Có Chắc Yêu Là Đây",
                Artist = "Sơn Tùng M-TP",
                Genre = "Pop",
                ReleaseDate = new DateTime(2020, 7, 10),
                FilePath = @"C:\DOAN\songsSonTung\cochacyeuladay.mp3",
                LyricsPath = @"C:\DOAN\lyricsSonTung\cochacyeuladay.txt"
            };

            Song song5 = new Song
            {
                Title = "Âm Thầm Bên Em",
                Artist = "Sơn Tùng M-TP",
                Genre = "Pop",
                ReleaseDate = new DateTime(2015, 8, 22),
                FilePath = @"C:\DOAN\songsSonTung\amthambenem.mp3",
                LyricsPath = @"C:\DOAN\lyricsSonTung\amthambenem.txt"
            };

            sonTungPlaylist.AddSong(song1);
            sonTungPlaylist.AddSong(song2);
            sonTungPlaylist.AddSong(song3);
            sonTungPlaylist.AddSong(song4);
            sonTungPlaylist.AddSong(song5);

            AddPlaylist(sonTungPlaylist);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Đã thêm playlist 'Sơn Tùng M-TP Collection' với 5 bài hát.");
            Console.ResetColor();
            return sonTungPlaylist;
        }

        // Thêm playlist Bolero mẫu
        public Playlist AddBoleroPlaylist()
        {
            Playlist boleroPlaylist = new Playlist { Name = "Bolero" };

            Song song1 = new Song
            {
                Title = "Duyên Phận",
                Artist = "Nhạc Sĩ Thái Châu",
                Genre = "Bolero",
                ReleaseDate = new DateTime(1983, 1, 1),
                FilePath = @"C:\DOAN\Baihatgoiy\duyen_phan.mp3",
                LyricsPath = @"C:\DOAN\Baihatgoiy\duyen_phan_lyrics.txt"
            };

            Song song2 = new Song
            {
                Title = "Mưa Đêm Tỉnh Nhỏ",
                Artist = "Nhạc Sĩ Hà Phương",
                Genre = "Bolero",
                ReleaseDate = new DateTime(1973, 1, 1),
                FilePath = @"C:\DOAN\Baihatgoiy\mua_dem_tinh_nho.mp3",
                LyricsPath = @"C:\DOAN\Baihatgoiy\mua_dem_tinh_nho_lyrics.txt"
            };

            Song song3 = new Song
            {
                Title = "Đắp Mộ Cuộc Tình",
                Artist = "Nhạc Sĩ Vũ Thanh",
                Genre = "Bolero",
                ReleaseDate = new DateTime(1995, 1, 1),
                FilePath = @"C:\DOAN\Baihatgoiy\dap_mo_cuoc_tinh.mp3",
                LyricsPath = @"C:\DOAN\Baihatgoiy\dap_mo_cuoc_tinh_lyrics.txt"
            };

            boleroPlaylist.AddSong(song1);
            boleroPlaylist.AddSong(song2);
            boleroPlaylist.AddSong(song3);

            AddPlaylist(boleroPlaylist);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Đã thêm playlist 'Bolero' với 3 bài hát mẫu.");
            Console.ResetColor();
            return boleroPlaylist;
        }

        //Hiển thị thông tin tất cả các bài hát trong tất cả các playlist
        public override void DisplayInfoSongs()
        {
            // Gọi phương thức DisplayInfoSongs từ lớp cha (Playlist)
            base.DisplayInfoSongs();
            Console.Clear();
            if (Playlists.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  Bạn chưa có playlist nào.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n🎵 Danh sách các playlist:");
            Console.ResetColor();

            for (int i = 0; i < Playlists.Count; i++)
            {
                var playlist = Playlists[i];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{i + 1}. {playlist.Name} ({playlist.Songs.Count} bài hát)");
                Console.ResetColor();

                if (playlist.Songs.Count > 0)
                {
                    foreach (var song in playlist.Songs)
                    {
                        Console.WriteLine($"   - {song.Title} by {song.Artist}");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("   (Playlist này không có bài hát nào)");
                    Console.ResetColor();
                }
            }
        }


    }
}
