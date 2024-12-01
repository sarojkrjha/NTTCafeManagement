using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, ICafeRepository cafeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _cafeRepository = cafeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByCafeAsync(string cafeName)
        {
            var employees = await _employeeRepository.GetEmployeesByCafeAsync(cafeName);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(string id, EmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                employee.Name = employeeDto.Name;
                employee.EmailAddress = employeeDto.EmailAddress;
                employee.PhoneNumber = employeeDto.PhoneNumber;
                employee.Gender = employeeDto.Gender;
              //  employee.DaysWorked = employeeDto.DaysWorked;
              //  employee.CafeName = employeeDto.CafeName;
                await _employeeRepository.UpdateAsync(employee);
            }
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
