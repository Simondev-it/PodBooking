namespace PB.APIService.RequestModel
{
    public class LoginRequest
    {
        public string Email { get; set; } // Đảm bảo rằng có thuộc tính Email
        public string Password { get; set; } // Đảm bảo rằng có thuộc tính Password
    }
}
