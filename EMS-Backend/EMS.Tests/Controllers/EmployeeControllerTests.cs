using EMS.API.Controllers;
using EMS.API.DTOs;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Controllers
{

    [TestFixture]
    public class EmployeesControllerTests
    {
        private Mock<IEmployeeRepository> _repoMock;
        private EmployeeService _service;
        private EmployeesController _controller;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
            _controller = new EmployeesController(_service);
        }

        /* -----------------------------
           GET BY ID
        ------------------------------*/

        [Test]
        public async Task GetEmployeeById_ValidId_ReturnsOk()
        {
            // Arrange
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

            // Act
            var result = await _controller.GetEmployeeById(1);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetEmployeeById_InvalidId_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetEmployeeById(999))
                     .ReturnsAsync((Employee)null);

            var result = await _controller.GetEmployeeById(999);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        /* -----------------------------
           CREATE
        ------------------------------*/

        [Test]
        public async Task CreateEmployee_Valid_ReturnsOk()
        {
            var dto = new EmployeeRequestDto
            {
                FirstName = "Rahul",
                LastName = "Sharma",
                Email = "rahul@test.com",
                Department = "HR",
                Status = "Active"
            };

            _repoMock.Setup(r => r.EmailExists(dto.Email))
                     .ReturnsAsync(false);

            var result = await _controller.CreateEmployee(dto);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            _repoMock.Verify(r => r.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public async Task CreateEmployee_DuplicateEmail_ReturnsConflict()
        {
            var dto = new EmployeeRequestDto
            {
                Email = "duplicate@test.com"
            };

            _repoMock.Setup(r => r.EmailExists(dto.Email))
                     .ReturnsAsync(true);

            var result = await _controller.CreateEmployee(dto);

            Assert.That(result, Is.InstanceOf<ConflictObjectResult>());
        }

        /* -----------------------------
           UPDATE
        ------------------------------*/

        [Test]
        public async Task UpdateEmployee_Valid_ReturnsOk()
        {
            var existing = new Employee
            {
                Id = 1,
                FirstName = "Old"
            };

            var dto = new EmployeeRequestDto
            {
                FirstName = "Updated"
            };

            _repoMock.Setup(r => r.GetEmployeeById(1))
                     .ReturnsAsync(existing);

            var result = await _controller.UpdateEmployee(1, dto);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public async Task UpdateEmployee_InvalidId_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetEmployeeById(999))
                     .ReturnsAsync((Employee)null);

            var result = await _controller.UpdateEmployee(999, new EmployeeRequestDto());

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        /* -----------------------------
           DELETE
        ------------------------------*/

        [Test]
        public async Task DeleteEmployee_Valid_ReturnsOk()
        {
            var emp = new Employee { Id = 1 };

            _repoMock.Setup(r => r.GetEmployeeById(1))
                     .ReturnsAsync(emp);

            var result = await _controller.DeleteEmployee(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            _repoMock.Verify(r => r.DeleteAsync(emp), Times.Once);
        }

        [Test]
        public async Task DeleteEmployee_InvalidId_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetEmployeeById(999))
                     .ReturnsAsync((Employee)null);

            var result = await _controller.DeleteEmployee(999);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        /* -----------------------------
           GET ALL
        ------------------------------*/

        [Test]
        public async Task GetEmployees_ReturnsOk()
        {
            var query = new EmployeeQueryParamsDto
            {
                Page = 1,
                PageSize = 5
            };

            _repoMock.Setup(r => r.GetAllEmployees(query))
                     .ReturnsAsync((new List<Employee>(), 0));

            var result = await _controller.GetEmployees(query);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
