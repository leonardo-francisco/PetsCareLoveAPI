using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Examination
{
    public interface IExaminationService
    {
        Task<ExaminationDto> GetExaminationByIdAsync(Guid examinationId);
        Task<IEnumerable<ExaminationDto>> GetExaminationsByPetIdAsync(Guid petId);
        Task PrescribeExaminationAsync(ExaminationDto examinationDto);
        Task UpdateExaminationAsync(ExaminationDto examinationDto);
        Task UpdateExaminationResultAsync(ExaminationResultDto examinationResultDto);
    }
}
