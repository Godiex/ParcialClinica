using System;

namespace Application.Http.Requests
{
    public class CareStaffRequest : PeopleRequest
    {
        public string Type { get; set; }
        public UserRequest User { get; set; }
    }
}
