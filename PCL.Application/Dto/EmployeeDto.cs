using PCL.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
            RegistrationCode = GenerateRegistrationCode.GenerateCode();
        }

        public Guid Id { get; set; }
        public string RegistrationCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ServiceType { get; set; }
        public List<Guid>? AppointmentIds { get; set; } = new List<Guid>();
    }
}
