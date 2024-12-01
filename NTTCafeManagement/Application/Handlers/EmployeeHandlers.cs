using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class EmployeeHandlers :
        IRequestHandler<CreateEmployeeCommand, string>,
        IRequestHandler<UpdateEmployeeCommand>,
        IRequestHandler<DeleteEmployeeCommand>,
        IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;

        public EmployeeHandlers(IEmployeeRepository employeeRepository, ICafeRepository cafeRepository)
        {
            _employeeRepository = employeeRepository;
            _cafeRepository = cafeRepository;
        }

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _cafeRepository.GetByIdAsync(request.CafeId);
            if (cafe == null)
                throw new Exception("Cafe not found");

            var employee = new Domain.Entities.Employee(Guid.NewGuid().ToString(), request.Name, request.EmailAddress, request.PhoneNumber, request.Gender );
            employee.Cafe = cafe;
             
            await _employeeRepository.AddAsync(employee);
            return employee.Id;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
                throw new Exception("Employee not found");

            var cafe = await _cafeRepository.GetByIdAsync(request.CafeId);
            if (cafe == null)
                throw new Exception("Cafe not found");

            employee.Name = request.Name;
            employee.EmailAddress = request.EmailAddress;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Gender = request.Gender;
            employee.Cafe = cafe;
           // employee.StartDate = request.StartDate;

            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
                throw new Exception("Employee not found");

            await _employeeRepository.DeleteAsync(request.Id);
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetEmployeesByCafeAsync(request.CafeName);
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                Gender = e.Gender,
               // DaysWorked = (int)(DateTime.Now - e.StartDate.GetValueOrDefault()).TotalDays,
                CafeName = e.Cafe.Name
            });
        }
    }
}
