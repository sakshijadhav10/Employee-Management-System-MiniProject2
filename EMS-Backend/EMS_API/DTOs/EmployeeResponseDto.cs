namespace EMS.API.DTOs
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime UpdatedAt { get; set; }
    }
}
