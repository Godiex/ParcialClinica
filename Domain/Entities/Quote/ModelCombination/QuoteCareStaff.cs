using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuoteCareStaff
    {
        public int IdCareStaff { get; set; }
        public CareStaff CareStaff { get; set; }
        public int IdQuote { get; set; }
        public Quote Quote { get; set; }

    }
}
