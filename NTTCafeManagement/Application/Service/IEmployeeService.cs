using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeByIdAsync(string id);
        Task<IEnumerable<EmployeeDto>> GetEmployeesByCafeAsync(string cafeName);
        Task CreateEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(string id, EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(string id);
    }
}
