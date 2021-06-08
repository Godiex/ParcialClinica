using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Http.Requests
{
    public class PatientRequest
    {
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public DirectionRequest Direction { get; set; }
    }
}
