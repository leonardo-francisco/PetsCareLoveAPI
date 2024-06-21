using PCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Domain.Interfaces
{
    public interface IExaminationRepository
    {
        Task<Examination> GetExaminationByIdAsync(Guid examinationId);
        Task<IEnumerable<Examination>> GetExaminationsByPetIdAsync(Guid petId);
        //Task<IEnumerable<Examination>> GetExaminationsByVeterinarianIdAsync(Guid veterinarianId);
        //Task<IEnumerable<ExaminationResult>> GetExaminationResultsByVeterinarianIdAsync(Guid veterinarianId);
        Task PrescribeExaminationAsync(Examination examination);
        Task UpdateExaminationAsync(Examination examination);
        Task UpdateExaminationResultAsync(ExaminationResult examinationResult);
    }
}
