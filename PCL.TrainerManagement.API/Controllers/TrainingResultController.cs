using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.TrainingResult;

namespace PCL.TrainerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingResultController : ControllerBase
    {
        private readonly ITrainingResultService _trainingResultService;

        public TrainingResultController(ITrainingResultService trainingResultService)
        {
            _trainingResultService = trainingResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainings()
        {
            var trainings = await _trainingResultService.GetAllAsync();
            return Ok(trainings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(Guid id)
        {
            var training = await _trainingResultService.GetByIdAsync(id);
            return Ok(training);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender(TrainingResultDto trainingResDto)
        {
            if (trainingResDto == null) return BadRequest();

            await _trainingResultService.CreateAsync(trainingResDto);

            return Ok(new
            {
                Message = "Resultado de treino criado com sucesso",
                Result = trainingResDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraining(TrainingResultDto trainingResDto)
        {
            if (trainingResDto == null) return BadRequest();

            await _trainingResultService.UpdateAsync(trainingResDto);

            return Ok(new
            {
                Message = "Resultado de treino atualizado com sucesso",
                Result = trainingResDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(Guid id)
        {
            await _trainingResultService.DeleteAsync(id);
            return NoContent();
        }
    }
}
