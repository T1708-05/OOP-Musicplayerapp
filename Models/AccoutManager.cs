using MusicPlayerApp.Models;
using MusicPlayerApp.Data;

namespace MusicPlayerApp.Managers
{
    public class AccountManager
    {
        private List<Account> accounts;

        public AccountManager()
        {
            // Tải danh sách tài khoản từ tệp JSON khi khởi tạo
            accounts = DataStorage.LoadAccounts();

            // Nếu accounts là null, khởi tạo danh sách mới
            if (accounts == null)
            {
                accounts = new List<Account>();
            }
        }

        // Nhập thông tin từ người dùng
        private string UserInput(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{prompt}: ");
            Console.ResetColor();
            return Console.ReadLine();
        }

        // Nhập ngày sinh từ người dùng
        private DateTime UserDateInput(string prompt)
        {
            DateTime result;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{prompt} (dd/MM/yyyy): ");
            Console.ResetColor();
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out result))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Ngày sinh không hợp lệ.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{prompt} (dd/MM/yyyy): ");
                Console.ResetColor();
            }
            return result;
        }

        // Kiểm tra đăng ký
        public bool Register(string username, string password, User userInfo)
        {
            if (accounts.Exists(acc => acc.Username == username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Tên đăng nhập đã tồn tại.");
                Console.ResetColor();
                return false;
            }

            Account newAccount = new Account(username, password, userInfo);
            accounts.Add(newAccount);
            DataStorage.SaveAccounts(accounts); // Lưu lại sau khi thêm tài khoản mới
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ Đăng ký thành công!");
            Console.ResetColor();
            return true;
        }

        // Phương thức đăng nhập
        public Account Login(string username, string password)
        {
            Account account = accounts.Find(acc => acc.Username == username);
            if (account != null && account.VerifyPassword(password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Đăng nhập thành công!");
                Console.ResetColor();

                return account;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Tên đăng nhập hoặc mật khẩu không đúng.");
                Console.ResetColor();
                return null;
            }
        }

        // Tài khoản test
        public void AccountTest()
        {
            if (accounts.Exists(acc => acc.Username == "admin"))
                return;

            User sampleUser = new User
            {
                Name = "ThongVT",
                NgaySinh = DateTime.Now,
                PhoneNumbers = "0123456789"
            };
            Register("admin", "admin", sampleUser);
        }

        // Đăng ký tài khoản
        public void UserRegister()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== 📝 ĐĂNG KÝ TÀI KHOẢN =====");
            Console.ResetColor();
            string username = UserInput("👤 Tên đăng nhập");
            string password = UserInput("🔒 Mật khẩu");

            string name = UserInput("📛 Họ và tên");
            DateTime ngaySinh = UserDateInput("🎂 Ngày sinh");
            string phoneNumber = UserInput("📞 Số điện thoại");

            User newUser = new User
            {
                Name = name,
                NgaySinh = ngaySinh,
                PhoneNumbers = phoneNumber
            };

            Register(username, password, newUser);
        }

        // Người dùng đăng nhập
        public Account UserLogin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== 🔑 ĐĂNG NHẬP =====");
            Console.ResetColor();
            string username = UserInput("👤 Tên đăng nhập");
            string password = UserInput("🔒 Mật khẩu");

            return Login(username, password);
        }
        public void ChangePassword(Account account)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== 🔒 ĐỔI MẬT KHẨU =====");
            Console.ResetColor();

            Console.Write("🔑 Nhập mật khẩu hiện tại: ");
            string currentPassword = Console.ReadLine();

            if (account.VerifyPassword(currentPassword))
            {
                Console.Write("🔑 Nhập mật khẩu mới: ");
                string newPassword = Console.ReadLine();

                Console.Write("🔑 Xác nhận mật khẩu mới: ");
                string confirmPassword = Console.ReadLine();

                if (newPassword == confirmPassword)
                {
                    account.SetPassword(newPassword); // Cập nhật mật khẩu trong đối tượng
                    DataStorage.SaveAccounts(accounts); // Lưu lại danh sách tài khoản vào JSON

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✅ Đổi mật khẩu thành công!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Mật khẩu xác nhận không khớp.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Mật khẩu hiện tại không đúng.");
                Console.ResetColor();
            }
        }

        // Lưu lại danh sách tài khoản (sau khi thay đổi mật khẩu hoặc playlist)
        public void SaveAccounts()
        {
            DataStorage.SaveAccounts(accounts);
        }
    }
}
