using EMS.API.DTOs;
using EMS.API.Models;

namespace EMS.API.Services
{
    public interface IEmployeeRepository
    {
        Task<(List<Employee> Data, int TotalCount)> GetAllEmployees(EmployeeQueryParamsDto query);
        Task<Employee?> GetEmployeeById(int id);
        Task<DashboardSummaryDto?> GetDashboard();
        Task AddAsync(Employee emp);
        Task UpdateAsync(Employee emp);
        Task DeleteAsync(Employee emp);
        Task<bool> EmailExists(string email);
        Task<bool> EmailExistsForOther(int id, string email);
    }
}
