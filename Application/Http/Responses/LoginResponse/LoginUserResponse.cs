namespace Application.Http.Responses
{
    public class LoginUserResponse
    {
        public string Username { get; set; }

        public LoginUserResponse(string username)
        {
            Username = username;
        }
    }
}