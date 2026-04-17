using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        //GET ALL (Paged)
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeQueryParamsDto query)
        {
            var result = await _service.GetAllEmployees(query);
            return Ok(result); 
        }

        // GET BY ID
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _service.GetEmployeeById(id);

            if (employee == null)
                return NotFound(new { message = "Employee not found" });

            var response = new EmployeeResponseDto
            {
                Id=employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Department = employee.Department,
                Designation = employee.Designation,
                Salary = employee.Salary,
                JoinDate = employee.JoinDate,
                Status = employee.Status
            };

            return Ok(response);
        }

        // DASHBOARD
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _service.GetDashboardAsync();
            return Ok(result);
        }

        // CREATE
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                Designation = dto.Designation,
                Salary = dto.Salary,
                JoinDate = dto.JoinDate,
                Status = dto.Status,
                CreatedAt = DateTime.Now
            };

            var success = await _service.CreateAsync(employee);

            if (!success)
                return Conflict(new { message = "Email already exists" });

            //  Return DTO instead of entity
            var response = new EmployeeResponseDto
            {
                Id=employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Department = employee.Department,
                Designation = employee.Designation,
                Salary = employee.Salary,
                JoinDate = employee.JoinDate,
                Status = employee.Status
            };

            return Ok(new
            {
                message = "Employee created successfully",
                data = response
            });
        }

        // UPDATE
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeRequestDto dto)
        {
            var existingEmployee = await _service.GetEmployeeById(id);

            if (existingEmployee == null)
                return NotFound(new { message = "Employee not found" });

            // Update fields
            existingEmployee.FirstName = dto.FirstName;
            existingEmployee.LastName = dto.LastName;
            existingEmployee.Email = dto.Email;
            existingEmployee.Phone = dto.Phone;
            existingEmployee.Department = dto.Department;
            existingEmployee.Designation = dto.Designation;
            existingEmployee.JoinDate = dto.JoinDate;
            existingEmployee.Salary = dto.Salary;
            existingEmployee.Status = dto.Status;
            existingEmployee.UpdatedAt = DateTime.Now;

            var success = await _service.UpdateAsync(existingEmployee);

            if (!success)
                return Conflict(new { message = "Email already exists" });


            //  Return DTO
            var response = new EmployeeResponseDto
            {
                Id=existingEmployee.Id,
                FirstName = existingEmployee.FirstName,
                LastName = existingEmployee.LastName,
                Email = existingEmployee.Email,
                Phone = existingEmployee.Phone,
                Department = existingEmployee.Department,
                Designation = existingEmployee.Designation,
                Salary = existingEmployee.Salary,
                JoinDate = existingEmployee.JoinDate,
                Status = existingEmployee.Status
            };

            return Ok(new
            {
                message = "Employee updated successfully",
                data = response
            });
        }

        // DELETE
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existingEmployee = await _service.GetEmployeeById(id);

            if (existingEmployee == null)
                return NotFound(new { message = "Employee not found" });

            await _service.DeleteAsync(existingEmployee);

            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
