using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient : People
    {
        public DateTime DateOfBirth { get; set; }
        public int age { get; set; }
        public string Telephone { get; set; }
        public Direction Direction { get; set; }

        public Patient(DateTime dateOfBirth, int age, string telephone, Direction direction, string name, string surname) : base(name, surname)
        {
            DateOfBirth = dateOfBirth;
            this.age = age;
            Telephone = telephone;
            Direction = direction;
        }
    }
}
