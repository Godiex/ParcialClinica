using Application.Http.Requests;
using System;

namespace Application.Http.Requests
{
    public class PatientRequest : PeopleRequest
    {
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public DirectionRequest Direction { get; set; }
    }
}
