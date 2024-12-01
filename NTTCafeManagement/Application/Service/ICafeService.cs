using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface ICafeService
    {
        Task<CafeDto> GetCafeByIdAsync(Guid id);
        Task<IEnumerable<CafeDto>> GetCafesByLocationAsync(string location);
        Task CreateCafeAsync(CafeDto cafeDto);
        Task UpdateCafeAsync(Guid id, CafeDto cafeDto);
        Task DeleteCafeAsync(Guid id);
    }
}
