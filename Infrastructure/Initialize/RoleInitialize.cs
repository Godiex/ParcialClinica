using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Initialize
{
    public static class RoleInitialize
    {
        public static void InitializeRole(MyContext context)
        {
            var roles = new List<Role>();
            roles.Add(new Role {  Name = "PERSONAL DE ATENCION" });
            roles.Add( new Role { Name = "ADMINISTRADOR" });
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
    }
}
