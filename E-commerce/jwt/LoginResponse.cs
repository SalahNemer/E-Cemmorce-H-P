namespace V1.jwt
{
    public class LoginResponse
    {
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public int Role {  get; set; }
        public int AccountStatus { get; set; }

        public LoginResponse(string errorMessage, string token, int role, int accountStatus)
        {
            ErrorMessage = errorMessage;
            Token = token;
            Role = role;
            AccountStatus = accountStatus;
        }
    }
}
