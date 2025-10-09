using Entity_Framework.Data;
using Entity_Framework.Models;
using Entity_Framework.Services;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new CompanyDbContext();

            await context.Database.MigrateAsync();

            await SeedDatabaseAsync(context);

            var employeeService = new EmployeeService(context);

            await DemonstrateCrudOperationsAsync(employeeService);
        }

        static async Task SeedDatabaseAsync(CompanyDbContext context)
        {
            // Check if database already has data
            if (await context.Departments.AnyAsync())
            {
                Console.WriteLine("Database already contains data, skipping seeding.\n");
                return;
            }

            Console.WriteLine("Seeding database with initial data...");

            // Seed Departments
            var departments = new[]
            {
                new Department { Name = "Engineering", Description = "Software development and technical infrastructure", Budget = 500000m },
                new Department { Name = "Sales", Description = "Revenue generation and client acquisition", Budget = 300000m },
                new Department { Name = "Marketing", Description = "Brand promotion and market analysis", Budget = 200000m },
                new Department { Name = "Human Resources", Description = "Employee management and company culture", Budget = 150000m }
            };

            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();

            // Seed Employees
            var employees = new[]
            {
                new Employee { Name = "John Doe", Email = "john.doe@company.com", Salary = 75000m, HireDate = new DateTime(2022, 1, 15), Position = "Senior Developer", DepartmentId = 1 },
                new Employee { Name = "Jane Smith", Email = "jane.smith@company.com", Salary = 68000m, HireDate = new DateTime(2022, 3, 10), Position = "Frontend Developer", DepartmentId = 1 },
                new Employee { Name = "Mike Johnson", Email = "mike.johnson@company.com", Salary = 82000m, HireDate = new DateTime(2021, 8, 20), Position = "Sales Manager", DepartmentId = 2 },
                new Employee { Name = "Sarah Wilson", Email = "sarah.wilson@company.com", Salary = 71000m, HireDate = new DateTime(2022, 5, 8), Position = "Marketing Specialist", DepartmentId = 3 },
                new Employee { Name = "David Brown", Email = "david.brown@company.com", Salary = 79000m, HireDate = new DateTime(2021, 11, 3), Position = "HR Manager", DepartmentId = 4 }
            };

            context.Employees.AddRange(employees);
            await context.SaveChangesAsync();

            // Seed Projects
            var projects = new[]
            {
                new Project { Name = "Website Redesign", Description = "Complete overhaul of company website", StartDate = new DateTime(2024, 1, 1), Budget = 50000m, Status = "Active" },
                new Project { Name = "Mobile App Development", Description = "Native mobile application for iOS and Android", StartDate = new DateTime(2024, 2, 15), Budget = 80000m, Status = "Active" },
                new Project { Name = "Sales Automation", Description = "CRM integration and sales process automation", StartDate = new DateTime(2023, 10, 1), EndDate = new DateTime(2024, 3, 31), Budget = 35000m, Status = "Completed" }
            };

            context.Projects.AddRange(projects);
            await context.SaveChangesAsync();

            Console.WriteLine("Database seeding completed successfully!\n");
        }

        static async Task DemonstrateCrudOperationsAsync(EmployeeService employeeService)
        {
            Console.WriteLine("===CRUD Operation Demo===\n");

            // Creating new Employee
            Console.WriteLine("1. Creating new Employee...");
            var newEmployee = new Employee
            {
                Name = "Naufal Farras",
                Email = "naufal@test.com",
                Position = "Frontend Developer",
                DepartmentId = 1,
                Salary = 500000m,
                HireDate = new DateTime(2024, 5, 12)
            };

            try
            {
                var createdEmployee = await employeeService.CreateEmployeeAsync(newEmployee);
                Console.WriteLine($"✔ Successful creating employee {createdEmployee.Name} (ID: {createdEmployee.Id})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to creating employee: {ex.Message}");
            }

            // Get All Employees
            Console.WriteLine("\n2. Reading Employees Data...");
            var employees = await employeeService.GetAllEmployeesAsync();
            Console.WriteLine($"✔ Found {employees.Count} employees: ");
            foreach (var employee in employees)
            {
                Console.WriteLine($"  - {employee.Name} | {employee.Position} in {employee.Department.Name}");
            }

            // Get Employee by Id
            Console.WriteLine("\n3. Get First Employee Data...");
            if (employees.Count > 0)
            {
                var firstEmployee = employees.First();
                Console.WriteLine($"✔ Retrieved Employee by ID: {firstEmployee.Id}");
                Console.WriteLine($"  Name: {firstEmployee.Name}");
                Console.WriteLine($"  Email: {firstEmployee.Email}");
                Console.WriteLine($"  Position: {firstEmployee.Position}");
                Console.WriteLine($"  Department: {firstEmployee.Department.Name}");
                Console.WriteLine($"  Salary: {firstEmployee.Salary:F2}");
            }

            // Updating Employee Data By Id
            Console.WriteLine("\n4. Updating Last Employee Data...");
            if (employees.Count > 0)
            {
                var lastEmployee = employees.Last();
                var originalSalary = lastEmployee.Salary;

                lastEmployee.Salary = originalSalary + 10000;
                lastEmployee.Position = $"Senior {lastEmployee.Position}";

                var updatedEmployee = await employeeService.UpdateEmployeeAsync(lastEmployee.Id, lastEmployee);
                if (lastEmployee != null)
                {
                    Console.WriteLine($"✔ Update Successful for {updatedEmployee.Name}:");
                    Console.WriteLine($"  Salary: {originalSalary:F2} ➡ {updatedEmployee.Salary:F2}");
                    Console.WriteLine($"  Position: {updatedEmployee.Position}");
                }
            }

            // Deactivating an Employee
            Console.WriteLine("\n5. Deactivating Employee...");
            if (employees.Count > 5)
            {
                var employeeToDeactivate = employees.Last();
                var deactivated = await employeeService.DeactivateEmployeeAsync(employeeToDeactivate.Id);
                if (deactivated)
                    Console.WriteLine($"✔ Deactivated employee: {employeeToDeactivate.Name}");
            }

            Console.WriteLine("\n===CRUD Operation Demo Completed===");
        }
    }
}