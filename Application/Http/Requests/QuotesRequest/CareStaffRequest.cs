using System;

namespace Application.Http.Requests
{
    public class CareStaffRequest : PeopleRequest
    {
        public string Type { get; set; }
        public UserRequest User { get; set; }
    }

    public class CareStaffRequestUpdate : PeopleRequest
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
