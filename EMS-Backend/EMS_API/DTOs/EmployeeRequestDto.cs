using EMS.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs
{
    public class EmployeeRequestDto
    {

        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone is required"), RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be 10 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage ="Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Salary is required"), Range(1, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "JoinDate is required")]
        [NotFutureDate(ErrorMessage = "Join date cannot be greater than today")]
        public DateTime JoinDate { get; set; }

        [Required(ErrorMessage ="Status is required")]
        public string Status { get; set; } = "Active";

    }
}
