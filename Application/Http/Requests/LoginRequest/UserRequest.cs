using Domain.Entities;
using System.Collections.Generic;

namespace Application.Http.Requests
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<int> IdRoles { get; set; }

        public User MapUser() 
        {
            User user = new User
            {
                Username = Username,
                Password = Password
            };
            return user;
        }
    }
}