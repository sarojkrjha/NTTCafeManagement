using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class CafeService : ICafeService
    {
        private readonly ICafeRepository _cafeRepository;

        public CafeService(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<CafeDto> GetCafeByIdAsync(Guid id)
        {
            var cafe = await _cafeRepository.GetByIdAsync(id);
            if (cafe == null)
            {
                throw new KeyNotFoundException("Cafe not found");
            }

            return new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Description = cafe.Description,
                Location = cafe.Location,
                Logo = cafe.Logo
            };
        }

        public async Task<IEnumerable<CafeDto>> GetCafesByLocationAsync(string location)
        {
            var cafes = await _cafeRepository.GetAllAsync(location);
            return cafes.Select(cafe => new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Description = cafe.Description,
                Location = cafe.Location,
                Logo = cafe.Logo
            });
        }

        public async Task CreateCafeAsync(CafeDto cafeDto)
        {
            var cafe = new Cafe(cafeDto.Name, cafeDto.Description, cafeDto.Location, cafeDto.Logo);
             
            await _cafeRepository.AddAsync(cafe);
        }

        public async Task UpdateCafeAsync(Guid id, CafeDto cafeDto)
        {
            var cafe = await _cafeRepository.GetByIdAsync(id);
            if (cafe == null)
            {
                throw new KeyNotFoundException("Cafe not found");
            }

            cafe.Name = cafeDto.Name;
            cafe.Description = cafeDto.Description;
            cafe.Location = cafeDto.Location;
            cafe.Logo = cafeDto.Logo;

            await _cafeRepository.UpdateAsync(cafe);
        }

        public async Task DeleteCafeAsync(Guid id)
        {
            var cafe = await _cafeRepository.GetByIdAsync(id);
            if (cafe == null)
            {
                throw new KeyNotFoundException("Cafe not found");
            }

            await _cafeRepository.DeleteAsync(cafe.Id);
        }
    }
}
