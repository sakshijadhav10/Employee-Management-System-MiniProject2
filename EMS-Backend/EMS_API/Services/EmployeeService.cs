using EMS.API.DTOs;
using EMS.API.Models;

namespace EMS.API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) { 
            _employeeRepository= employeeRepository;
        }

        public async Task<PagedResultDto<EmployeeResponseDto>> GetAllEmployees(EmployeeQueryParamsDto query)
        {
            var (employees, totalCount) = await _employeeRepository.GetAllEmployees(query);
            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
            var data = employees.Select(e => new EmployeeResponseDto
            {
                Id=e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                Department = e.Department,
                Designation = e.Designation,
                Salary = e.Salary,
                JoinDate = e.JoinDate,
                Status = e.Status
            }).ToList();

            return new PagedResultDto<EmployeeResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = totalPages,
                HasNextPage = query.Page < totalPages,
                HasPrevPage = query.Page > 1
            };
        }

        public async Task<Employee?> GetEmployeeById(int id) => await _employeeRepository.GetEmployeeById(id);

        // public async Task<List<DashboardDto>> GetDashboard() => await _employeeRepository.GetDashboard();
        public async Task<DashboardSummaryDto> GetDashboardAsync()
        {
            return await _employeeRepository.GetDashboard();
        }
        public async Task<bool> CreateAsync(Employee employee)
        {
            if (await _employeeRepository.EmailExists(employee.Email))
            {
                return false;
            }
            await _employeeRepository.AddAsync(employee);
            return true;

        }


        public async Task<bool> UpdateAsync(Employee emp)
        {

            if (await _employeeRepository.EmailExistsForOther(emp.Id, emp.Email))
            {
                return false; 
            }

            await _employeeRepository.UpdateAsync(emp);
            return true;
        }
        public async Task DeleteAsync(Employee emp)
            => await _employeeRepository.DeleteAsync(emp);

        public async Task<bool> EmailExistsForOther(int id, string email)
        {
            return await _employeeRepository.EmailExistsForOther(id, email);
        }
    }
}
