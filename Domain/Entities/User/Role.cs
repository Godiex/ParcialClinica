using Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Role : Entity<int>
    {
        public string Name { get; set; }

        #region Relaciones entre tablas
        public List<User> Users { get; set; }
        #endregion Relaciones entre tablas
    }
}
