using Domain.Entities;
using System;

namespace Application.Http.Requests
{
    public class PatientRequest : PeopleRequest
    {
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public DirectionRequest Direction { get; set; }

        public Patient MapPatient()
        {
            Patient patient = new Patient
            {
                Identification = Identification,
                Name = Name,
                Surname = Surname,
                DateOfBirth = DateOfBirth,
                Photo = Photo,
                Telephone = Telephone,
                Direction = MapDirection(),
                State = true
            };
            return patient;
        }

        private Direction MapDirection()
        {
            Direction direction = new Direction(Direction.Nomenclature, Direction.City, Direction.Neighborhood);
            return direction;
        }

    }

    public class PatientRequestUpdate : PeopleRequest
    {
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public DirectionRequest Direction { get; set; }

        public Patient MapPatient(Patient patient)
        {
            patient.Identification = Identification;
            patient.Name = Name;
            patient.Surname = Surname;
            patient.DateOfBirth = DateOfBirth;
            patient.Photo = Photo;
            patient.Telephone = Telephone;
            patient.Direction = MapDirection();
            return patient;
        }

        private Direction MapDirection()
        {
            Direction direction = new Direction(Direction.Nomenclature, Direction.City, Direction.Neighborhood);
            return direction;
        }

    }
}
