using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateEmployeeCommand : IRequest<string>
    {
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public Guid CafeId { get; set; }
        public DateTime StartDate { get; set; }

        public CreateEmployeeCommand(string name, string emailAddress, string phoneNumber, string gender, Guid cafeId, DateTime startDate)
        {
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Gender = gender;
            CafeId = cafeId;
            StartDate = startDate;
        }
    }
}
