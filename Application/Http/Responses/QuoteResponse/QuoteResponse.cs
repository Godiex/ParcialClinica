using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Http.Responses.QuoteResponse
{
    public class QuoteResponse
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string State { get; set; }
        public PatientResponse Patient { get; set; }
        public List<CareStaffResponse> CareStaff { get; set; }

        public QuoteResponse(Quote quote)
        {
            StartTime = quote.StartTime;
            EndTime = quote.EndTime;
            State = quote.State;
        }

        public QuoteResponse Include(List<CareStaff> careStaff, Patient patient) 
        {
            CareStaff = careStaff.ConvertAll(c => new CareStaffResponse(c)).ToList();
            Patient = new PatientResponse(patient);
            return this;
        }

    }
}
