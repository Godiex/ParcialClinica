using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Http.Requests
{
    public class QuoteRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string IdentificationPatient { get; set; }
        public List<CareStaffQuoteRequest> CareStaff { get; set; }

        public Quote MapQuote() 
        {
            Quote quote = new Quote
            {
                StartTime = StartTime,
                EndTime = EndTime,
                Date = DateTime.Now,
                State = "Asignado"
            };
            return quote;
        }
    }

    public class QuoteRequestUpdated
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string Observation { get; set; }

    }
}
