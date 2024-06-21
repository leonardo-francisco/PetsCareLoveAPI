using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Training;

namespace PCL.TrainerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainings()
        {
            var trainings = await _trainingService.GetAllAsync();
            return Ok(trainings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(Guid id)
        {
            var training = await _trainingService.GetByIdAsync(id);
            return Ok(training);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender(TrainingDto trainingDto)
        {
            if (trainingDto == null) return BadRequest();

            await _trainingService.CreateAsync(trainingDto);

            return Ok(new
            {
                Message = "Treino criado com sucesso",
                Training = trainingDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraining(TrainingDto trainingDto)
        {
            if (trainingDto == null) return BadRequest();

            await _trainingService.UpdateAsync(trainingDto);

            return Ok(new
            {
                Message = "Treino atualizado com sucesso",
                Training = trainingDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(Guid id)
        {
            await _trainingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
