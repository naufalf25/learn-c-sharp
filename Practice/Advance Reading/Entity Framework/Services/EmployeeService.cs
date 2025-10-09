using Entity_Framework.Data;
using Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework.Services;

public class EmployeeService
{
    private readonly CompanyDbContext _context;

    public EmployeeService(CompanyDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == employee.Email);

        if (existingEmployee != null)
            throw new InvalidOperationException($"Employee with email {employee.Email} already exists");

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    // READ
    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .OrderBy(e => e.Name)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Projects)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    // UPDATE
    public async Task<Employee?> UpdateEmployeeAsync(int id, Employee employee)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (existingEmployee == null) return null;

        existingEmployee.Name = employee.Name;
        existingEmployee.Email = employee.Email;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.Position = employee.Position;

        await _context.SaveChangesAsync();
        return existingEmployee;
    }

    // DELETE (Soft Delete)
    public async Task<bool> DeactivateEmployeeAsync(int id)
    {
        var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (existingEmployee == null) return false;

        existingEmployee.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}