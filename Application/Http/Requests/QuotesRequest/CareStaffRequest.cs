using Domain.Entities;
using System;

namespace Application.Http.Requests
{
    public class CareStaffRequest : PeopleRequest
    {
        public string Type { get; set; }
        public UserRequest User { get; set; }

        public CareStaff MapCareStaff()
        {
            return new CareStaff
            {
                Identification = Identification,
                Name = Name,
                Surname = Surname,
                Photo = Photo,
                State = true,
                Type = Type
            };
        }
    }

    public class CareStaffQuoteRequest 
    {
        public string IdentificationCareStaff { get; set; }
    }

    public class CareStaffRequestUpdate : PeopleRequest
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public CareStaff MapCareStaff(CareStaff careStaff)
        {
            careStaff.Name = Name;
            careStaff.Surname = Surname;
            careStaff.Photo = Photo;
            careStaff.Type = Type;
            return careStaff;
        }

    }
}
