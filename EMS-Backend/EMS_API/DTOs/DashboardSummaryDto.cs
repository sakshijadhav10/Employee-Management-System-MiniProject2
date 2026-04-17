using EMS.API.Models;

namespace EMS.API.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int InactiveEmployees { get; set; }
        public int TotalDepartments { get; set; }

        public List<DepartmentDto> DepartmentBreakdown { get; set; }
        public List<Employee> RecentEmployees { get; set; }
    }
}
