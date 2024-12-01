 using Domain.Entities;
using Domain.Interfaces;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafesController : ControllerBase
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CafesController(ICafeRepository cafeRepository, IEmployeeRepository employeeRepository)
        {
            _cafeRepository = cafeRepository;
            _employeeRepository = employeeRepository;
        }

        // GET: /cafes?location=<location>
        [HttpGet]
        public async Task<IActionResult> GetCafes([FromQuery] string? location)
        {
            var cafes = await _cafeRepository.GetCafesAsync(location);
            var result = cafes.Select(c => new
            {
                c.Name,
                c.Description,
                Employees = c.EmployeeCafes.Count,
                c.Logo,
                c.Location,
                c.Id
            }).OrderByDescending(c => c.Employees);

            return Ok(result);
        }

        // POST: /cafe
        [HttpPost]
        public async Task<IActionResult> CreateCafe([FromBody] Cafe cafe)
        {
            if (string.IsNullOrWhiteSpace(cafe.Name) || string.IsNullOrWhiteSpace(cafe.Description) || string.IsNullOrWhiteSpace(cafe.Location))
                return BadRequest("Name, Description, and Location are required fields.");

            await _cafeRepository.AddCafeAsync(cafe);
            return CreatedAtAction(nameof(GetCafes), new { id = cafe.Id }, cafe);
        }

        // PUT: /cafe
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCafe(Guid id, [FromBody] Cafe updatedCafe)
        {
            var cafe = await _cafeRepository.GetCafeByIdAsync(id);
            if (cafe == null)
                return NotFound();

            cafe.Name = updatedCafe.Name ?? cafe.Name;
            cafe.Description = updatedCafe.Description ?? cafe.Description;
            cafe.Logo = updatedCafe.Logo ?? cafe.Logo;
            cafe.Location = updatedCafe.Location ?? cafe.Location;

            await _cafeRepository.UpdateCafeAsync(cafe);
            return NoContent();
        }

        // DELETE: /cafe/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            var cafe = await _cafeRepository.GetCafeByIdAsync(id);
            if (cafe == null)
                return NotFound();

            await _cafeRepository.DeleteCafeAsync(id);
            return NoContent();
        }
    }
}
