using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Trainer;

namespace PCL.TrainerManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAllTrainers()
        {
            var trainers = await _trainerService.GetAllTrainersAsync();
            return Ok(trainers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainerDto>> GetTrainerById(Guid id)
        {
            var trainer = await _trainerService.GetTrainerByIdAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return Ok(trainer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrainer(TrainerDto trainerDto)
        {
            await _trainerService.CreateTrainerAsync(trainerDto);
            return Ok(new
            {
                Message = "Adestrador criado com sucesso",
                Trainer = trainerDto
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrainer(Guid id, TrainerDto trainerDto)
        {
            if (id != trainerDto.Id)
            {
                return BadRequest();
            }

            await _trainerService.UpdateTrainerAsync(trainerDto);
            return Ok(new
            {
                Message = "Adestrador atualizado com sucesso",
                Trainer = trainerDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrainer(Guid id)
        {
            await _trainerService.DeleteTrainerAsync(id);
            return NoContent();
        }

        [HttpGet("{trainerId}/appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByTrainerId(Guid trainerId)
        {
            var appointments = await _trainerService.GetAppointmentsByTrainerIdAsync(trainerId);
            return Ok(appointments);
        }

        [HttpGet("{trainerId}/pets")]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetPetsByTrainerId(Guid trainerId)
        {
            var pets = await _trainerService.GetPetsByTrainerIdAsync(trainerId);
            return Ok(pets);
        }

        [HttpPost("prescribe-training")]
        public async Task<ActionResult> PrescribeTraining(TrainingDto trainingDto)
        {
            await _trainerService.PrescribeTrainingAsync(trainingDto);
            return Ok();
        }

        [HttpGet("{trainerId}/training-results")]
        public async Task<ActionResult<IEnumerable<TrainingResultDto>>> GetTrainingResultsByTrainerId(Guid trainerId)
        {
            var trainingResults = await _trainerService.GetTrainingResultsByTrainerIdAsync(trainerId);
            return Ok(trainingResults);
        }

        [HttpGet("training-records/{petId}")]
        public async Task<ActionResult<TrainingRecordDto>> GetTrainingRecordByPetId(Guid petId)
        {
            var trainingRecord = await _trainerService.GetTrainingRecordByPetIdAsync(petId);
            if (trainingRecord == null)
            {
                return NotFound();
            }
            return Ok(trainingRecord);
        }
    }
}
