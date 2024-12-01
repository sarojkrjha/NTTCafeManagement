using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
        public string? CafeName { get; set; }

        public GetEmployeesQuery(string? cafeName)
        {
            CafeName = cafeName;
        }
    }
}
