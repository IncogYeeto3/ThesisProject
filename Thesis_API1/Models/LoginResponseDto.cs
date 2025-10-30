namespace Thesis_API1.Models
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public string Number { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
