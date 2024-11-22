using MusicPlayerApp.Models;
using Newtonsoft.Json; // Cài đặt thư viện này nếu chưa có: dotnet add package Newtonsoft.Json

namespace MusicPlayerApp.Data
{
    public static class DataStorage 
    {
        private static string FilePath = @"C:\DOAN\accounts.json";

        // luu danh sach vao tep tin
         public static void SaveAccounts(List<Account> accounts)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(accounts, Formatting.Indented);
                File.WriteAllText(FilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Lỗi lưu dữ liệu: {ex.Message}");
                Console.ResetColor();
            }
        }

        public static List<Account> LoadAccounts()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string jsonData = File.ReadAllText(FilePath);
                    return JsonConvert.DeserializeObject<List<Account>>(jsonData);
                }
                else
                {
                    return new List<Account>();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Lỗi đọc dữ liệu: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }
    }
}
