using EMS.API.Data;
using EMS.API.DTOs;
using EMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Services
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<(List<Employee> Data, int TotalCount)> GetAllEmployees(EmployeeQueryParamsDto query)
        {
            var employees = _context.Employees.AsQueryable();

            // SEARCH
            if (!string.IsNullOrEmpty(query.Search))
            {
                var search = query.Search.ToLower();
                employees = employees.Where(e =>
                    (e.FirstName + " " + e.LastName).ToLower().Contains(search) ||
                    e.Email.ToLower().Contains(search));
            }

            //FILTER: Department
            if (!string.IsNullOrEmpty(query.Department))
            {
                employees = employees.Where(e =>
                    e.Department.ToLower() == query.Department.ToLower());
            }

            //FILTER: Status
            if (!string.IsNullOrEmpty(query.Status))
            {
                employees = employees.Where(e =>
                    e.Status.ToLower() == query.Status.ToLower());
            }

            // SORTING 
            var sortBy = query.SortBy?.ToLower() ?? "name";
            var sortDir = query.SortDir?.ToLower() ?? "asc";

            employees = (sortBy, sortDir) switch
            {
                ("id","asc")=>employees.OrderBy(e=>e.Id),
                ("id","desc")=>employees.OrderByDescending(e=>e.Id),
                ("salary", "asc") => employees.OrderBy(e => e.Salary),
                ("salary", "desc") => employees.OrderByDescending(e => e.Salary),

                ("joindate", "asc") => employees.OrderBy(e => e.JoinDate),
                ("joindate", "desc") => employees.OrderByDescending(e => e.JoinDate),

                ("name", "desc") => employees.OrderByDescending(e => e.FirstName),
                //("name","asc")=>employees.OrderBy(e=>e.FirstName),
                _ => employees.OrderBy(e => e.FirstName)
            };

            // TOTAL COUNT (before pagination)
            var totalCount = await employees.CountAsync();

            // PAGINATION SAFE
            var page = query.Page <= 0 ? 1 : query.Page;
            var pageSize = Math.Min(query.PageSize <= 0 ? 10 : query.PageSize, 100);

            var data = await employees
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // CORRECT RETURN
            return (data, totalCount);
        }

        public async Task<Employee?> GetEmployeeById (int id)=> await _context.Employees.FindAsync(id);

        public async Task<DashboardSummaryDto?> GetDashboard()
        {
            var dashboard=new DashboardSummaryDto();

            //Dashboard KPIs
           dashboard.TotalEmployees=await _context.Employees.CountAsync();

            dashboard.ActiveEmployees = await _context.Employees.CountAsync(e => e.Status == "Active");

            dashboard.InactiveEmployees = await _context.Employees.CountAsync(e => e.Status == "Inactive");
            dashboard.TotalDepartments = await _context.Employees.Select(e => e.Department).Distinct().CountAsync();

            //Department Breakdown
            dashboard.DepartmentBreakdown = await _context.Employees.GroupBy(e => e.Department).Select(g => new DepartmentDto
            {
                Department = g.Key,
                Count = g.Count(),
            })
            .ToListAsync();

            //Recent 5 Employees
            dashboard.RecentEmployees = await _context.Employees.OrderByDescending(e => e.Id).Take(5).ToListAsync();

            return dashboard;
        }


        public async Task AddAsync(Employee? employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee? employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee? employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> EmailExists(string email)=>await _context.Employees.AnyAsync(e=>e.Email== email);
        public async Task<bool> EmailExistsForOther(int id, string email)
        {
            return await _context.Employees
                .AnyAsync(e => e.Email == email && e.Id != id);
        }

    }
}
