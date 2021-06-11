using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CareStaff : People
    {
        public string Type { get; set; }
        public bool WorkStatus { get; set; }
        public User User { get; set; }

        #region Relaciones entre tablas

        public List<Quote> Quotes { get; set; }

        #endregion Relaciones entre tablas

        public CareStaff(string type, string name, string surname) : base(name, surname)
        {
            Type = type;
        }
        public CareStaff()
        {

        }

    }
}
