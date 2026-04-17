using EMS.API.Data;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Integration
{
    [TestFixture]
    public class EmployeeIntegrationTests
    {
        private AppDbContext _context;
        private EmployeeService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ✅ fresh DB every test
                .Options;

            _context = new AppDbContext(options);

            // ✅ Important: ensure seed data is applied
            _context.Database.EnsureCreated(); // applies seed data

            var repo = new EmployeeRepository(_context);
            _service = new EmployeeService(repo);
        }
        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
        /* -----------------------------
           GET (Seed Data Check)
        ------------------------------*/

        [Test]
        public async Task GetEmployee_ShouldReturnSeededEmployee()
        {
            var emp = await _service.GetEmployeeById(1);

            Assert.That(emp, Is.Not.Null);
            Assert.That(emp.FirstName, Is.EqualTo("Priya")); // from seed
        }

        /* -----------------------------
           ADD EMPLOYEE
        ------------------------------*/

        [Test]
        
        public async Task AddEmployee_ShouldPersist()
        {
            var emp = new Employee
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test.user@xyz.com",
                Phone = "9999999999",
                Department = "HR",
                Designation = "Tester",
                Salary = 30000,
                JoinDate = DateTime.Now,
                Status = "Active",
                CreatedAt = DateTime.Now
            };

            var result = await _service.CreateAsync(emp);

            Assert.That(result, Is.True);

            var saved = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == "test.user@xyz.com");

            Assert.That(saved, Is.Not.Null);
        }

        /* -----------------------------
           EMAIL UNIQUENESS
        ------------------------------*/

        [Test]
        public async Task AddEmployee_DuplicateEmail_ShouldFail()
        {
            // Priya already exists in seed data
            var emp = new Employee
            {
                Email = "priya.sharma@xyz.com"
            };

            var result = await _service.CreateAsync(emp);

            Assert.That(result, Is.False);
        }

        /* -----------------------------
           DELETE EMPLOYEE
        ------------------------------*/

        [Test]
        public async Task DeleteEmployee_ShouldRemove()
        {
            var emp = await _service.GetEmployeeById(1);

            await _service.DeleteAsync(emp);

            var deleted = await _service.GetEmployeeById(1);

            Assert.That(deleted, Is.Null);
        }

        /* -----------------------------
           UPDATE EMPLOYEE
        ------------------------------*/

        [Test]
        public async Task UpdateEmployee_ShouldModifyData()
        {
            var emp = await _service.GetEmployeeById(1);

            emp.FirstName = "UpdatedName";

            await _service.UpdateAsync(emp);

            var updated = await _service.GetEmployeeById(1);

            Assert.That(updated.FirstName, Is.EqualTo("UpdatedName"));
        }

        /* -----------------------------
           DASHBOARD TEST
        ------------------------------*/

        [Test]
        public async Task Dashboard_ShouldReturnCorrectCounts()
        {
            var result = await _service.GetDashboardAsync();

            Assert.That(result, Is.Not.Null);

            var total = await _context.Employees.CountAsync();
            var active = await _context.Employees.CountAsync(e => e.Status == "Active");
            var inactive = await _context.Employees.CountAsync(e => e.Status == "Inactive");

            Assert.That(result.TotalEmployees, Is.EqualTo(total));
            Assert.That(result.ActiveEmployees, Is.EqualTo(active));
            Assert.That(result.InactiveEmployees, Is.EqualTo(inactive));
        }
    }
}
