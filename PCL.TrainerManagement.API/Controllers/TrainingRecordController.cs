using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.TrainingRecord;

namespace PCL.TrainerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingRecordController : ControllerBase
    {
        private readonly ITrainingRecordService _trainingRecordService;

        public TrainingRecordController(ITrainingRecordService trainingRecordService)
        {
            _trainingRecordService = trainingRecordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrainings()
        {
            var trainings = await _trainingRecordService.GetAllAsync();
            return Ok(trainings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(Guid id)
        {
            var training = await _trainingRecordService.GetByIdAsync(id);
            return Ok(training);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender(TrainingRecordDto trainingRecDto)
        {
            if (trainingRecDto == null) return BadRequest();

            await _trainingRecordService.CreateAsync(trainingRecDto);

            return Ok(new
            {
                Message = "Registro de treino criado com sucesso",
                Record = trainingRecDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTraining(TrainingRecordDto trainingRecDto)
        {
            if (trainingRecDto == null) return BadRequest();

            await _trainingRecordService.UpdateAsync(trainingRecDto);

            return Ok(new
            {
                Message = "Registro de treino atualizado com sucesso",
                Record = trainingRecDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(Guid id)
        {
            await _trainingRecordService.DeleteAsync(id);
            return NoContent();
        }
    }
}
