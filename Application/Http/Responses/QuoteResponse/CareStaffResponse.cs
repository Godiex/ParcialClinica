using Domain.Entities;
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

        public CareStaffResponse(CareStaff careStaff)
        {
            Identification = careStaff.Identification;
            Name = careStaff.Name;
            Surname = careStaff.Surname;
            Photo = careStaff.Photo;
            WorkStatus = careStaff.WorkStatus;
        }
    }
}
