using Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; }

        #region Relaciones entre tablas
        [NotMapped] public int IdCareStaff { get; set; }
        public CareStaff CareStaff { get; set; }

        #endregion Relaciones entre tablas

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void AddRangeRoles(List<Role> roles) 
        {
            Roles.AddRange(roles);
        }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}