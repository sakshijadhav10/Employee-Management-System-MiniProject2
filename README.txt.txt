Employee Management System (EMS)- MiniProject2

Project:
The Employee Management System (EMS) is a full-stack web application developed using .NET 8 Web API for backend and HTML, CSS, and JavaScript for frontend.
It allows users to manage employee records with authentication and CRUD operations.


Developed By:
Name : Sakshi Dhanaji Jadhav
Batch : CTS_B7_Python_Group 

Tech Stack:
Backend: .NET 8 Web API
Frontend: HTML, CSS, JavaScript
Database: SQL Server
Authentication: JWT (JSON Web Token)
Tools: Visual Studio / VS Code, Postman
 
Prerequisites

Make sure the following are installed:

.NET 8 SDK
SQL Server
Visual Studio / VS Code
Browser (Chrome recommended)


Dependencies
Entity Framework Core
JWT Authentication
Swagger (for API testing)


To Login As An Admin Use:
username = admin
password=admin123

To Login As An Viewer Use:
username=viewer
password=viewer123


Frontend Setup
Open EMS-Frontend/index.html
Run using Live Server

How to run frontend test: npm test

how to run backend app:

Backend Setup:

open CLI : 
dotnet clean 
dotnet build
dotnet run

Or Open Project in Visual Studio:
 


Running the Published EMS.API Project

Open terminal in the  deploy folder:
cd deploy

Run the API using the following command:
dotnet EMS.API.dll --urls "http://localhost:5005"

Open the browser and access Swagger UI:
http://localhost:5005/swagger

The API will now be running and ready for testing.



Steps to Run :
1.Extract ZIP file
2.Open EMS-Backend -> EMS-Backend.sln -> Open solution in Visual Studio
3.Restore NuGet Packages
4.Update DB using appsettings.json
5.Auto Migration is applied So run  : dotnet run
6.Run the application(F5) 



API Testing Using Swagger
Swagger URL:
http://localhost:5005/swagger


API Testing Using Postman:

Register
Method:POST
http://localhost:5005/api/auth/register

Login
Method:POST
http://localhost:5005/api/auth/login

GET Employees
method: GET
http://localhost:5005/api/employees

Get Employee By Id:
Method:GET
http://localhost:5005/api/employees/{id}

Create Employee
Method: POST
http://localhost:5005/api/employees

Update Employee
Method: PUT
http://localhost:5005/api/employees/{id}

Delete Employee:
Method:DELETE
http://localhost:5005/api/employees/{id}





