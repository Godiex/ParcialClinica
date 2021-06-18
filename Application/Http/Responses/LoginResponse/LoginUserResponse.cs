using Domain.Entities;
using System.Collections.Generic;

namespace Application.Http.Responses
{
    public class LoginUserResponse
    {
        public string Username { get; set; }
        public List<RoleResponse> Roles { get; set; }

        public LoginUserResponse(string username, List<Role> roles)
        {
            Username = username;
            Roles = roles.ConvertAll(r => new RoleResponse(r));
        }

    }

    public class RoleResponse
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public RoleResponse(Role role)
        {
            Name = role.Name;
            Id = role.Id;
        }
    }
}