using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EMS.Tests.Services
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _repoMock;
        private EmployeeService _service;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
        }

        // ✅ GET BY ID - VALID
        [Test]
        public async Task GetEmployeeById_ValidId_ReturnsEmployee()
        {
            var emp = new Employee
            {
                Id = 1,
                FirstName = "Anjali",
                LastName = "Mehta",
                Email = "anjali@test.com",
                Status = "Active"
            };

            _repoMock.Setup(r => r.GetEmployeeById(1))
                     .ReturnsAsync(emp);

            var result = await _service.GetEmployeeById(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.FirstName, Is.EqualTo("Anjali"));

            _repoMock.Verify(r => r.GetEmployeeById(1), Times.Once);
        }

        // ✅ GET BY ID - INVALID
        [Test]
        public async Task GetEmployeeById_InvalidId_ReturnsNull()
        {
            _repoMock.Setup(r => r.GetEmployeeById(999))
                     .ReturnsAsync((Employee?)null);

            var result = await _service.GetEmployeeById(999);

            Assert.That(result, Is.Null);
        }

        // ✅ CREATE SUCCESS
        [Test]
        public async Task CreateAsync_ValidEmployee_ReturnsTrue()
        {
            var emp = new Employee
            {
                FirstName = "Rahul",
                LastName = "Sharma",
                Email = "rahul@test.com",
                Department = "HR",
                Status = "Active"
            };

            _repoMock.Setup(r => r.EmailExists(emp.Email))
                     .ReturnsAsync(false);

            var result = await _service.CreateAsync(emp);

            Assert.That(result, Is.True);

            _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        // ✅ CREATE DUPLICATE EMAIL
        [Test]
        public async Task CreateAsync_DuplicateEmail_ReturnsFalse()
        {
            var emp = new Employee
            {
                Email = "duplicate@test.com"
            };

            _repoMock.Setup(r => r.EmailExists(emp.Email))
                     .ReturnsAsync(true);

            var result = await _service.CreateAsync(emp);

            Assert.That(result, Is.False);
        }
    }
}
