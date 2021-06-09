using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Http.Requests
{
    public class QuoteRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Patient Patient { get; set; }
        private List<CareStaff> CareStaff { get; set; }

        public void AddCareStaff(CareStaff careStaff)
        {
            CareStaff.Add(careStaff);
        }
    }
}
