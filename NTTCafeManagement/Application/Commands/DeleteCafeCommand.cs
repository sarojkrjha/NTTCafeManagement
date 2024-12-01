using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteCafeCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteCafeCommand(Guid id)
        {
            Id = id;
        }
    }
}
