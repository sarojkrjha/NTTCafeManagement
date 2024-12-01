using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(string? cafeName);
        Task<Employee?> GetEmployeeByIdAsync(string id);
        Task AddEmployeeAsync(Employee employee, Guid cafeId, DateTime startDate);
        Task UpdateEmployeeAsync(Employee employee, Guid? cafeId, DateTime? startDate);
        Task DeleteEmployeeAsync(string id);
    }
}
