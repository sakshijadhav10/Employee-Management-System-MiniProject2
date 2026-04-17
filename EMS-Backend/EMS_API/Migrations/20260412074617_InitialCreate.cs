using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Department", "Designation", "Email", "FirstName", "JoinDate", "LastName", "Phone", "Salary", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Software Engineer", "priya.sharma@xyz.com", "Priya", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sharma", "9876543210", 750000m, "Active", new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR", "HR Executive", "rahul.patil@xyz.com", "Rahul", new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patil", "9876543211", 500000m, "Active", new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2020, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", "Marketing Manager", "sneha.joshi@xyz.com", "Sneha", new DateTime(2020, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joshi", "9876543212", 650000m, "Inactive", new DateTime(2020, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Accountant", "amit.k@xyz.com", "Amit", new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kulkarni", "9876543213", 550000m, "Active", new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operations", "Operations Manager", "neha.g@xyz.com", "Neha", new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gupta", "9876543214", 600000m, "Active", new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Backend Developer", "vikas.y@xyz.com", "Vikas", new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yadav", "9876543215", 800000m, "Active", new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2021, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "HR", "Recruiter", "riya.v@xyz.com", "Riya", new DateTime(2021, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Verma", "9876543216", 450000m, "Inactive", new DateTime(2021, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2020, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", "SEO Specialist", "karan.s@xyz.com", "Karan", new DateTime(2020, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singh", "9876543217", 520000m, "Active", new DateTime(2020, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2019, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Finance Analyst", "anjali.m@xyz.com", "Anjali", new DateTime(2019, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mehta", "9876543218", 580000m, "Active", new DateTime(2019, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Operations", "Operations Executive", "rohit.s@xyz.com", "Rohit", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shah", "9876543219", 480000m, "Active", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "Frontend Developer", "pooja.n@xyz.com", "Pooja", new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nair", "9876543220", 720000m, "Inactive", new DateTime(2022, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marketing", "Content Strategist", "arjun.r@xyz.com", "Arjun", new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rao", "9876543221", 540000m, "Active", new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2018, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engineering", "HR Manager", "kavita.i@xyz.com", "Kavita", new DateTime(2018, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iyer", "9876543222", 650000m, "Active", new DateTime(2018, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2017, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Senior Accountant", "suresh.p@xyz.com", "Suresh", new DateTime(2017, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pillai", "9876543223", 700000m, "Active", new DateTime(2017, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2020, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finance", "Logistics Manager", "deepak.c@xyz.com", "Deepak", new DateTime(2020, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chauhan", "9876543224", 620000m, "Inactive", new DateTime(2020, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$Oa5Q2WjFBrz3SAeRMRCvBeiSV1m2r.23RSzBLol2SHi3LZ/KQyaq6", "Admin", "admin" },
                    { 2, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$AqR5.6LGtD3J1CGAXcDOKO.62DCfCle0hdUxYtohodd7tGsSfMI4.", "Viewer", "viewer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
