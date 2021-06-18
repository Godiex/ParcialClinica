using Domain.Entities;
using System.Collections.Generic;

namespace Application.Http.Responses
{
    public class LoginUserResponse
    {
        public string Username { get; set; }
        public List<RoleResponse> Roles { get; set; }
        public string Identification { get; set; }

        public LoginUserResponse(User user, List<Role> roles)
        {
            Username = user.Username;
            Identification = user.CareStaff != null ? user.CareStaff.Identification : "ADMIN";
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