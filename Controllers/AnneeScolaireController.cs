using api_gestion_ecole.Dtos;
using api_gestion_ecole.Dtos.AnneeScolaire;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api_gestion_ecole.Controllers
{
    [Route("api/anneescolaires")]
    [ApiController]
    public class AnneeScolaireController:ControllerBase
    {
        private readonly IAnneeScolaireRepository _anneeScolaireRepository;
        public AnneeScolaireController(IAnneeScolaireRepository anneeScolaireRepository)
        {
            _anneeScolaireRepository = anneeScolaireRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var annee = (await _anneeScolaireRepository.GetAllAsync())
                            .Select(a=>a.ToAnneeScolaireDto());
            return Ok(annee);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var anneeScolaire = await _anneeScolaireRepository.GetByIdAsync(id);
            if(anneeScolaire == null) return NotFound(new {message = "Année scolaire introuvable"});
            return Ok(anneeScolaire.ToAnneeScolaireDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAnneeScolaireDto createAnneeScolaireDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(await _anneeScolaireRepository.IsAnneeScolaireExist(0,createAnneeScolaireDto.Designation))
            {
                return Conflict(new {message = "Cette année scolaire existe déjà"});
            }

            var annneeScolaire = await _anneeScolaireRepository.CreateAsync(createAnneeScolaireDto);
            return StatusCode(201, annneeScolaire?.ToAnneeScolaireDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAnneeScolaireDto updateAnneeScolaireDto)
        {
            var anneeScolaire = await _anneeScolaireRepository.UpdateAsync(id, updateAnneeScolaireDto);

            if(await _anneeScolaireRepository.IsAnneeScolaireExist(id, updateAnneeScolaireDto.Designation))
            {
                return Conflict(new {message = "Cette année scolaire existe déjà"});
            }
            if(anneeScolaire == null) return NotFound(new {message = "Année scolaire introuvable"});
            return Ok(anneeScolaire.ToAnneeScolaireDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var anneeScolaire = await _anneeScolaireRepository.DeleteAsync(id);
            if(anneeScolaire == null) return NotFound(new {message = "Année scolaire introuvable"});
            return NoContent();
        }
    }
}