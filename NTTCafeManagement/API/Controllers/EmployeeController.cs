 using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: /employees?cafe=<cafe>
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? cafe)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(cafe);
            var now = DateTime.UtcNow;  // Avoid using DateTime.Now, use UTC instead

            var result = employees
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Email_Address,
                    e.Phone_Number,
                    // Ensure the calculation happens on the server side
                    DaysWorked = e.EmployeeCafes
                                .Select(ec => (now-ec.StartDate).Days)
                                .DefaultIfEmpty(0)  // Default to 0 if no cafes
                                .Max(),
                    // Get the first café or default to an empty string if no café is assigned
                    Cafe = e.EmployeeCafes
                        .FirstOrDefault()?.Cafe.Name ?? string.Empty
                })
                .OrderByDescending(e => e.DaysWorked)  // Sort by the maximum days worked
                .ToList();

            return Ok(result);
        }

        // POST: /employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee, [FromQuery] Guid cafeId, [FromQuery] DateTime startDate)
        {
            if (string.IsNullOrWhiteSpace(employee.Id) || string.IsNullOrWhiteSpace(employee.Name) ||
                string.IsNullOrWhiteSpace(employee.Email_Address) || string.IsNullOrWhiteSpace(employee.Phone_Number) ||
                string.IsNullOrWhiteSpace(employee.Gender))
                return BadRequest("All fields are required.");

            await _employeeRepository.AddEmployeeAsync(employee, cafeId, startDate);
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
        }

        // PUT: /employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] Employee updatedEmployee, [FromQuery] Guid? cafeId, [FromQuery] DateTime? startDate)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            employee.Name = updatedEmployee.Name ?? employee.Name;
            employee.Email_Address = updatedEmployee.Email_Address ?? employee.Email_Address;
            employee.Phone_Number = updatedEmployee.Phone_Number ?? employee.Phone_Number;
            employee.Gender = updatedEmployee.Gender ?? employee.Gender;

            await _employeeRepository.UpdateEmployeeAsync(employee, cafeId, startDate);
            return NoContent();
        }

        // DELETE: /employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
