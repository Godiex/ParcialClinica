using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Http.Responses.QuoteResponse
{
    public class CareStaffResponse : PeopleResponse
    {
        public bool WorkStatus { get; set; }
    }
}
