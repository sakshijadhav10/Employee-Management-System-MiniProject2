namespace EMS.API.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Username { get; set; }
        public string Role { get; set; } = "Viewer";

        public string Token { get; set; }
    }
}
