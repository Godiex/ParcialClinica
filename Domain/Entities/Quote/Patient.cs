using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Patient")]
    public class Patient : People
    {
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public int IdDireccion { get; set; }
        public Direction Direction { get; set; }

        #region Relacion entre tablas
        public int I { get; set; }
        #endregion Relacion entre tablas    

        public Patient(DateTime dateOfBirth, string telephone, Direction direction, string name, string surname) : base(name, surname)
        {
            DateOfBirth = dateOfBirth;
            Telephone = telephone;
            Direction = direction;
        }

        public Patient()
        {

        }

        public int GetAge() 
        {
            DateTime now = DateTime.Today;
            int edad = DateTime.Today.Year - DateOfBirth.Year;
            return DateTime.Today < DateOfBirth.AddYears(edad) ? --edad : edad;
        }
    }
}
