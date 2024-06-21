using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TestandoSaporra.Context;
using TestandoSaporra.Models;

namespace TestandoSaporra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly PetCareContext _context;

        public PetController(PetCareContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _context.Pets.Find(_ => true).ToListAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(string id)
        {
            var pet = await _context.Pets.Find(breed => breed.Id == id).FirstOrDefaultAsync();
            return Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePet(Pet pet)
        {
            if (pet == null) return BadRequest();

            await _context.Pets.InsertOneAsync(pet);

            return Ok(new
            {
                Message = "Animal criado com sucesso",
                Animal = pet
            });
        }
    }
}
