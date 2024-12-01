using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public string Id { get; set; } = string.Empty; // Format: UIXXXXXXX
        public string Name { get; set; } = string.Empty;
        public string Email_Address { get; set; } = string.Empty;
        public string Phone_Number { get; set; } = string.Empty; // Starts with 8 or 9, 8 digits
        public string Gender { get; set; } = string.Empty; // Male or Female
        public virtual ICollection<EmployeeCafe> EmployeeCafes { get; set; } = new List<EmployeeCafe>();
    }
}
