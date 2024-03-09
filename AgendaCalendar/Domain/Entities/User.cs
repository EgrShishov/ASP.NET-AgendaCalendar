
using System.Text.Json.Serialization;

namespace AgendaCalendar.Domain.Entities
{
    public class User : Entity
    {
        public User() { }
        public User(string name, string password, string email) 
        {
            UserName = name;
            Password = password;
            Email = email;
        }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool Update(User newUser)
        {
            if (newUser == null) return false;

            UserName = newUser.UserName;
            Password = newUser.Password;
            Email = newUser.Email;

            return true;
        }

        public override string ToString()
        {
            return $"User: {UserName}, password: {Password}, email: {Email}";
        }
    }
}