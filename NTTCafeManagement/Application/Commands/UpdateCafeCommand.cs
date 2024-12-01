using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateCafeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string? Logo { get; set; }

        public UpdateCafeCommand(Guid id, string name, string description, string location, string? logo)
        {
            Id = id;
            Name = name;
            Description = description;
            Location = location;
            Logo = logo;
        }
    }
}
