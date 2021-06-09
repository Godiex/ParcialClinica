using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Direction : Entity<int>
    {
        public string Nomenclature { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }

        #region Relaciones entre tablas

        public int idPatient { get; set; }
        public virtual Patient Patient { get; set; }

        #endregion Relaciones entre tablas

        public Direction(string nomenclature, string city, string neighborhood)
        {
            Nomenclature = nomenclature;
            City = city;
            Neighborhood = neighborhood;
        }

        public Direction()
        {

        }
    }
}
