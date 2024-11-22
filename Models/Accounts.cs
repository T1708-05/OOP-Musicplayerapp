
using Newtonsoft.Json;

namespace MusicPlayerApp.Models
{
    public class Account
    {
        public string Username { get; set; }

        [JsonIgnore] // Bỏ qua khi serialize
        private string Password { get; set; }

        public string PlainPassword
        {
            get { return Password; }
            set { Password = value; }
        }

        public User UserInfo { get; set; }

        public Account() { } // Constructor mặc định cho việc deserialize

        public Account(string username, string password, User userInfo)
        {
            Username = username;
            SetPassword(password);
            UserInfo = userInfo;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public bool VerifyPassword(string password)
        {
            return Password ==password;
        }

    }
}
