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
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string State { get; set; }
        public PatientResponse Patient { get; set; }
        public List<CareStaffResponse> CareStaff { get; set; }

        public QuoteResponse(Quote quote)
        {
            Id = quote.Id;
            StartTime = quote.StartTime;
            EndTime = quote.EndTime;
            State = quote.State;
        }

        public QuoteResponse Include(List<CareStaff> careStaff, Patient patient) 
        {
            CareStaff = careStaff.ConvertAll(c => new CareStaffResponse(c)).ToList();
            Patient = new PatientResponse(patient).Include(patient.Direction);
            return this;
        }

    }

    public class QuoteAssignedToCareStaff
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string State { get; set; }
        public PatientResponse Patient { get; set; }

        public QuoteAssignedToCareStaff(Quote quote)
        {
            Id = quote.Id;
            StartTime = quote.StartTime;
            EndTime = quote.EndTime;
            State = quote.State;
        }

        public QuoteAssignedToCareStaff Include(Patient patient)
        {
            Patient = new PatientResponse(patient).Include(patient.Direction);
            return this;
        }

    }

}
