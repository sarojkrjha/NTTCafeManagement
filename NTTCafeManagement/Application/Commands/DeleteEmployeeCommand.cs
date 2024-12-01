using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public string Id { get; set; }

        public DeleteEmployeeCommand(string id)
        {
            Id = id;
        }
    }
}
