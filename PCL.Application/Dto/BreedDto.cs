using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Dto
{
    public class BreedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TypeAnimalId { get; set; }
        public TypeAnimalDto TypeAnimal { get; set; }
    }
}
