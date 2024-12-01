using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CafeHandlers :
        IRequestHandler<CreateCafeCommand, Guid>,
        IRequestHandler<UpdateCafeCommand>,
        IRequestHandler<DeleteCafeCommand>,
        IRequestHandler<GetCafesQuery, IEnumerable<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;

        public CafeHandlers(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Domain.Entities.Cafe(request.Name, request.Description, request.Location, request.Logo);
             
            await _cafeRepository.AddAsync(cafe);
            return cafe.Id;
        }

        public async Task Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _cafeRepository.GetByIdAsync(request.Id);
            if (cafe == null)
                throw new Exception("Cafe not found");

            cafe.Name = request.Name;
            cafe.Description = request.Description;
            cafe.Location = request.Location;
            cafe.Logo = request.Logo;

            await _cafeRepository.UpdateAsync(cafe);
        }

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _cafeRepository.GetByIdAsync(request.Id);
            if (cafe == null)
                throw new Exception("Cafe not found");

            await _cafeRepository.DeleteAsync(request.Id);
        }

        public async Task<IEnumerable<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeRepository.GetAllAsync(request.Location);
            return cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Location = c.Location,
                Logo = c.Logo,
                Employees = c.Employees.Count
            });
        }
    }
}
