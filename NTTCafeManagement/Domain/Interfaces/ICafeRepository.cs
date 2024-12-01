using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICafeRepository
    {
        Task<IEnumerable<Cafe>> GetCafesAsync(string? location);
        Task<Cafe?> GetCafeByIdAsync(Guid id);
        Task AddCafeAsync(Cafe cafe);
        Task UpdateCafeAsync(Cafe cafe);
        Task DeleteCafeAsync(Guid id);
    }
}
