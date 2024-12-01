using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string? cafeName)
        {
            var query = _context.Employee.Include(e => e.EmployeeCafes).ThenInclude(ec => ec.Cafe).AsQueryable();
            if (!string.IsNullOrEmpty(cafeName))
                query = query.Where(e => e.EmployeeCafes.Any(ec => ec.Cafe.Name.Equals(cafeName, StringComparison.OrdinalIgnoreCase)));

            return await query
                .Select(e => new Employee
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email_Address = e.Email_Address,
                    Phone_Number = e.Phone_Number,
                    Gender = e.Gender,
                    EmployeeCafes = e.EmployeeCafes
                })
                .OrderByDescending(e => e.EmployeeCafes.Max(ec => EF.Functions.DateDiffDay(ec.StartDate, DateTime.Now)))
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string id) =>
            await _context.Employee.Include(e => e.EmployeeCafes).FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddEmployeeAsync(Employee employee, Guid cafeId, DateTime startDate)
        {
            _context.Employee.Add(employee);
            _context.EmployeeCafe.Add(new EmployeeCafe { EmployeeId = employee.Id, CafeId = cafeId, StartDate = startDate });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee, Guid? cafeId, DateTime? startDate)
        {
            _context.Employee.Update(employee);
            if (cafeId.HasValue && startDate.HasValue)
            {
                var existingRelation = await _context.EmployeeCafe.FirstOrDefaultAsync(ec => ec.EmployeeId == employee.Id);
                if (existingRelation != null)
                {
                    existingRelation.CafeId = cafeId.Value;
                    existingRelation.StartDate = startDate.Value;
                    _context.EmployeeCafe.Update(existingRelation);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
