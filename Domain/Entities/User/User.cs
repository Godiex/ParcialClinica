using Domain.Base;

namespace Domain.Entities
{
    public class User : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        #region Relaciones entre tablas

        public int IdCareStaff { get; set; }
        public virtual CareStaff CareStaff { get; set; }

        #endregion Relaciones entre tablas

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User()
        {

        }
    }
}