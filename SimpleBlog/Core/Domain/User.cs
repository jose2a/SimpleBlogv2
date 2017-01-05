using System.Collections.Generic;

namespace SimpleBlog.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual IList<Role> Roles { get; set; }

        private const int WorkFactor = 13;

        public User()
        {
            Posts = new List<Post>();
            Roles = new List<Role>();
        }

        public virtual void SetPassword(string passsword)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(passsword, BCrypt.Net.BCrypt.GenerateSalt(WorkFactor));
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.CheckPassword(password, PasswordHash);
        }

        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", BCrypt.Net.BCrypt.GenerateSalt(WorkFactor));
        }
    }
}