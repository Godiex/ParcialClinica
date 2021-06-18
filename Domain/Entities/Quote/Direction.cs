using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped] public int IdPatient { get; set; }
        public Patient Patient { get; set; }
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
