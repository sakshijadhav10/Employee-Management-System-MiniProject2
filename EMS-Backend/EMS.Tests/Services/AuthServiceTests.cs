using EMS.API.Data;
using EMS.API.DTOs.Auth;
using EMS.API.Jwt;
using EMS.API.Models;
using EMS.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Tests.Services
{

    [TestFixture]
    public class AuthServiceTests
    {
        private AppDbContext _db;
        private Mock<ITokenService> _tokenMock;
        private AuthService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

           AppDbContext _db = new AppDbContext(options);

            // ✅ Seed users
            _db.Users.AddRange(
                new AppUser
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                },
                new AppUser
                {
                    Username = "viewer",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("viewer123"),
                    Role = "Viewer"
                }
            );

            _db.SaveChanges();

            // ✅ Mock TokenService
            _tokenMock = new Mock<ITokenService>();
            _tokenMock.Setup(t => t.GenerateToken(It.IsAny<AppUser>()))
                      .Returns("fake-jwt-token");

            _service = new AuthService(_db, _tokenMock.Object);
        }

        // VALID LOGIN
        [Test]
        public async Task Login_ValidCredentials_ReturnsTokenAndRole()
        {
            var result = await _service.Login(new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123"
            });

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Token, Is.Not.Null.And.Not.Empty);
            Assert.That(result?.Role, Is.EqualTo("Admin"));
        }

        // INVALID USERNAME
        [Test]
        public async Task Login_InvalidUsername_ReturnsNull()
        {
            var result = await _service.Login(new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123"
            });

            Assert.That(result, Is.Null);
        }

        // WRONG PASSWORD
        [Test]
        public async Task Login_WrongPassword_ReturnsNull()
        {
            var dto = new AuthRequestDto
            {
                Username = "admin",
                Password = "admin123"
            };

            var result = await _service.Login(dto);

            Assert.That(result, Is.Null);
        }
        //Register New User
        [Test]
        public async Task Register_NewUser_ReturnsSuccess()
        {
            var dto = new AuthRequestDto
            {
                Username = "newuser",
                Password = "123456",
                Role = "Viewer"
            };

            var result = await _service.Register(dto);

            Assert.That(result.Success, Is.True);
        }
    }

}
