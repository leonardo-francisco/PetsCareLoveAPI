using PCL.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL.Application.Services.Gender
{
    public interface IGenderService
    {
        Task<IEnumerable<GenderDto>> GetAllGendersAsync();
        Task<GenderDto> GetGenderByIdAsync(Guid id);
        Task CreateGenderAsync(GenderDto genderDto);
        Task UpdateGenderAsync(GenderDto genderDto);
        Task DeleteGenderAsync(Guid id);
    }
}
