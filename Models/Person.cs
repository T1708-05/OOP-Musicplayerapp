
namespace MusicPlayerApp.Models
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime NgaySinh { get; set; }
        public string PhoneNumbers { get; set; }

        public virtual void DisplayInfoUser()
        {
            Console.WriteLine($"Ten: {Name}, Ngay Sinh: {NgaySinh.ToShortDateString()}, So dien thoai: {PhoneNumbers}");
        }
    }
}
