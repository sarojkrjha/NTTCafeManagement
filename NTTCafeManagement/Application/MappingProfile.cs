using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to DTO
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.CafeName, opt => opt.MapFrom(src => src.Cafe.Name));

            CreateMap<Cafe, CafeDto>();

            // DTO to Domain
            CreateMap<EmployeeDto, Employee>();
            CreateMap<CafeDto, Cafe>();
        }
    }
}
