using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Http.Responses.QuoteResponse
{
    public class PatientResponse : PeopleResponse
    {
        public int Age { get; set; }
        public string Telephone { get; set; }
        public DirectionResponse DirectionResponse { get; set; }

        public PatientResponse( Patient patient )
        {
            Identification = patient.Identification;
            Name = patient.Name;
            Surname = patient.Surname;
            Photo = patient.Photo;
            Age = patient.GetAge();
            Telephone = patient.Telephone;
        }

        public PatientResponse Include (Direction direction)
        {
            if (direction != null) 
            {
                DirectionResponse = new DirectionResponse
                {
                    City = direction.City,
                    Neighborhood = direction.Neighborhood,
                    Nomenclature = direction.Nomenclature
                };
            }
            return this;
        }
    }
}
