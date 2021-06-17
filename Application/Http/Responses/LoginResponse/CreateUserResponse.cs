using System;

namespace Application.Http.Responses
{
    public class CreateUserResponse
    {
        public string UserName { get; set; }

        public CreateUserResponse(string userName)
        {
            UserName = userName;
        }
    }
}