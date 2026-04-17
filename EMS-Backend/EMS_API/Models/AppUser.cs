using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash {  get; set; }=null!;

        public string Role { get; set; } = "Viewer";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
