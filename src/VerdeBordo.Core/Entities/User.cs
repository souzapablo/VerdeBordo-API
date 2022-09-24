using VerdeBordo.Core.Entities.Base;
using VerdeBordo.Core.Enums;

namespace VerdeBordo.Core.Entities
{
    public class User : BaseEntity
    {
        #region Constructors

        public User(string username, string email, string password, Role role)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        #endregion

        #region Properties

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

        #endregion
    }
}