using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EMS.API.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; }= null!;
        public string Department {  get; set; }=null!;
        public string Designation { get; set; } = null!;

        [Precision(18,2)]
        public decimal Salary {  get; set; }
        public DateTime JoinDate {  get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
