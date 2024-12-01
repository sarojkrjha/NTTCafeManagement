using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public Guid CafeId { get; set; }
        public DateTime StartDate { get; set; }

        public UpdateEmployeeCommand(string id, string name, string emailAddress, string phoneNumber, string gender, Guid cafeId, DateTime startDate)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Gender = gender;
            CafeId = cafeId;
            StartDate = startDate;
        }
    }
}
