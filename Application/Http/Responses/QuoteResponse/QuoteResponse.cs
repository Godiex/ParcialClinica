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
        private List<CareStaffResponse> CareStaff { get; set; }
        public void AddCareStaff(CareStaff careStaff)
        {
            CareStaff.Add(careStaff);
        }
    }
}
