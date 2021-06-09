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

        public CareStaff(string type, string name, string surname) : base(name, surname)
        {
            Type = type;
        }

    }
}
