using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Utils
{
    public static class GenerateRegistrationCode
    {
        public static string GenerateCode()
        {
            Random random = new Random();
            int number = random.Next(1000, 9999);
            return $"PCL-{number}";
        }
    }
}
