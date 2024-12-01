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
    public class CafeRepository : ICafeRepository
    {
        private readonly AppDbContext _context;

        public CafeRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Cafe>> GetCafesAsync(string? location)
        {
            var query = _context.Cafe.Include(c => c.EmployeeCafes).AsQueryable();
            if (!string.IsNullOrEmpty(location))
                query = query.Where(c => c.Location.Equals(location, StringComparison.OrdinalIgnoreCase));

            return await query
                .OrderByDescending(c => c.EmployeeCafes.Count)
                .ToListAsync();
        }

        public async Task<Cafe?> GetCafeByIdAsync(Guid id) =>
            await _context.Cafe.Include(c => c.EmployeeCafes).FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddCafeAsync(Cafe cafe)
        {
            _context.Cafe.Add(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCafeAsync(Cafe cafe)
        {
            _context.Cafe.Update(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCafeAsync(Guid id)
        {
            var cafe = await GetCafeByIdAsync(id);
            if (cafe != null)
            {
                _context.Cafe.Remove(cafe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
