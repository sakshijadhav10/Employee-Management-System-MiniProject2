using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee>Employees { get; set; }
        public DbSet<AppUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Employee>().HasData(

            new Employee { Id = 1, FirstName = "Priya", LastName = "Sharma", Email = "priya.sharma@xyz.com", Phone = "9876543210", Department = "Engineering", Designation = "Software Engineer", Salary = 750000, JoinDate = new DateTime(2022, 6, 15), Status = "Active", CreatedAt = new DateTime(2022, 6, 15), UpdatedAt = new DateTime(2022, 6, 15) },

            new Employee { Id = 2, FirstName = "Rahul", LastName = "Patil", Email = "rahul.patil@xyz.com", Phone = "9876543211", Department = "HR", Designation = "HR Executive", Salary = 500000, JoinDate = new DateTime(2021, 4, 10), Status = "Active", CreatedAt = new DateTime(2021, 4, 10), UpdatedAt = new DateTime(2021, 4, 10) },

            new Employee { Id = 3, FirstName = "Sneha", LastName = "Joshi", Email = "sneha.joshi@xyz.com", Phone = "9876543212", Department = "Marketing", Designation = "Marketing Manager", Salary = 650000, JoinDate = new DateTime(2020, 8, 11), Status = "Inactive", CreatedAt = new DateTime(2020, 8, 11), UpdatedAt = new DateTime(2020, 8, 11) },

            new Employee { Id = 4, FirstName = "Amit", LastName = "Kulkarni", Email = "amit.k@xyz.com", Phone = "9876543213", Department = "Finance", Designation = "Accountant", Salary = 550000, JoinDate = new DateTime(2019, 5, 2), Status = "Active", CreatedAt = new DateTime(2019, 5, 2), UpdatedAt = new DateTime(2019, 5, 2) },

            new Employee { Id = 5, FirstName = "Neha", LastName = "Gupta", Email = "neha.g@xyz.com", Phone = "9876543214", Department = "Operations", Designation = "Operations Manager", Salary = 600000, JoinDate = new DateTime(2023, 2, 14), Status = "Active", CreatedAt = new DateTime(2023, 2, 14), UpdatedAt = new DateTime(2023, 2, 14) },

             new Employee { Id = 6, FirstName = "Vikas", LastName = "Yadav", Email = "vikas.y@xyz.com", Phone = "9876543215", Department = "Engineering", Designation = "Backend Developer", Salary = 800000, JoinDate = new DateTime(2022, 3, 10), Status = "Active", CreatedAt = new DateTime(2022, 3, 10), UpdatedAt = new DateTime(2022, 3, 10) },

            new Employee { Id = 7, FirstName = "Riya", LastName = "Verma", Email = "riya.v@xyz.com", Phone = "9876543216", Department = "HR", Designation = "Recruiter", Salary = 450000, JoinDate = new DateTime(2021, 7, 18), Status = "Inactive", CreatedAt = new DateTime(2021, 7, 18), UpdatedAt = new DateTime(2021, 7, 18) },

            new Employee { Id = 8, FirstName = "Karan", LastName = "Singh", Email = "karan.s@xyz.com", Phone = "9876543217", Department = "Marketing", Designation = "SEO Specialist", Salary = 520000, JoinDate = new DateTime(2020, 11, 21), Status = "Active", CreatedAt = new DateTime(2020, 11, 21), UpdatedAt = new DateTime(2020, 11, 21) },

            new Employee { Id = 9, FirstName = "Anjali", LastName = "Mehta", Email = "anjali.m@xyz.com", Phone = "9876543218", Department = "Finance", Designation = "Finance Analyst", Salary = 580000, JoinDate = new DateTime(2019, 9, 13), Status = "Active", CreatedAt = new DateTime(2019, 9, 13), UpdatedAt = new DateTime(2019, 9, 13) },

            new Employee { Id = 10, FirstName = "Rohit", LastName = "Shah", Email = "rohit.s@xyz.com", Phone = "9876543219", Department = "Operations", Designation = "Operations Executive", Salary = 480000, JoinDate = new DateTime(2023, 1, 2), Status = "Active", CreatedAt = new DateTime(2023, 1, 2), UpdatedAt = new DateTime(2023, 1, 2) },

            new Employee { Id = 11, FirstName = "Pooja", LastName = "Nair", Email = "pooja.n@xyz.com", Phone = "9876543220", Department = "Engineering", Designation = "Frontend Developer", Salary = 720000, JoinDate = new DateTime(2022, 5, 9), Status = "Inactive", CreatedAt = new DateTime(2022, 5, 9), UpdatedAt = new DateTime(2022, 5, 9) },

            new Employee { Id = 12, FirstName = "Arjun", LastName = "Rao", Email = "arjun.r@xyz.com", Phone = "9876543221", Department = "Marketing", Designation = "Content Strategist", Salary = 540000, JoinDate = new DateTime(2021, 12, 22), Status = "Active", CreatedAt = new DateTime(2021, 12, 22), UpdatedAt = new DateTime(2021, 12, 22) },

            new Employee { Id = 13, FirstName = "Kavita", LastName = "Iyer", Email = "kavita.i@xyz.com", Phone = "9876543222", Department = "Engineering", Designation = "HR Manager", Salary = 650000, JoinDate = new DateTime(2018, 10, 11), Status = "Active", CreatedAt = new DateTime(2018, 10, 11), UpdatedAt = new DateTime(2018, 10, 11) },

            new Employee { Id = 14, FirstName = "Suresh", LastName = "Pillai", Email = "suresh.p@xyz.com", Phone = "9876543223", Department = "Finance", Designation = "Senior Accountant", Salary = 700000, JoinDate = new DateTime(2017, 6, 30), Status = "Active", CreatedAt = new DateTime(2017, 6, 30), UpdatedAt = new DateTime(2017, 6, 30) },

            new Employee { Id = 15, FirstName = "Deepak", LastName = "Chauhan", Email = "deepak.c@xyz.com", Phone = "9876543224", Department = "Finance", Designation = "Logistics Manager", Salary = 620000, JoinDate = new DateTime(2020, 4, 4), Status = "Inactive", CreatedAt = new DateTime(2020, 4, 4), UpdatedAt = new DateTime(2020, 4, 4) }

 );
            //var adminPassword = BCrypt.Net.BCrypt.HashPassword("admin123");
            //var viewerPassword = BCrypt.Net.BCrypt.HashPassword("viewer123");
            modelBuilder.Entity<AppUser>()
                .HasData(
                new AppUser { Id = 1, Username = "admin", PasswordHash = "$2a$11$Oa5Q2WjFBrz3SAeRMRCvBeiSV1m2r.23RSzBLol2SHi3LZ/KQyaq6", Role = "Admin", CreatedAt = new DateTime(2026, 04, 12) },
                new AppUser { Id = 2, Username = "viewer", PasswordHash = "$2a$11$AqR5.6LGtD3J1CGAXcDOKO.62DCfCle0hdUxYtohodd7tGsSfMI4.", Role = "Viewer", CreatedAt = new DateTime(2024, 04, 12) }

                );
        }
    }
}
