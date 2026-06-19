using api_gestion_ecole.Dtos.FraisConcernerClasse;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/frais-concerner-classes")]
    [ApiController]
    public class FraisConcernerClasseController:ControllerBase
    {
        private readonly IFraisConcernerClasseRepository _repository;
        public FraisConcernerClasseController(IFraisConcernerClasseRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fraisConcernerClasses = await _repository.GetAllAsync();
            return Ok(fraisConcernerClasses.Select(c=>
                        c.ToFraisConcernerClasseDto()));
        }

        [HttpGet("{fraisId:int}/{classeId:int}/{anneeScolaireId}")]
        public async Task<IActionResult> GetById(int fraisId,int classeId, int anneeScolaireId)
        {
            var fraisConcernerClasse = await _repository.GetByIdAsync(fraisId, classeId, anneeScolaireId);

            if (fraisConcernerClasse == null)
                return NotFound();

            return Ok(fraisConcernerClasse.ToFraisConcernerClasseDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFraisConcernerClasseDto createFraisConcernerClasseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fraisExists =
                await _repository.IsFraisExistAsync(createFraisConcernerClasseDto.FraisId);

            if (!fraisExists)
                return BadRequest(new {message ="Le frais spécifié n'existe pas."});

            var classeExists =
                await _repository.IsClasseExistAsync(createFraisConcernerClasseDto.ClasseId);

            if (!classeExists)
                return BadRequest(new {message ="La classe spécifiée n'existe pas."});

            var anneeScolaireExists =
                await _repository.IsAnneeScolaireExistAsync(createFraisConcernerClasseDto.AnneeScolaireId);

            if (!anneeScolaireExists)
                return BadRequest(new {message = "L'année scolaire spécifiée n'existe pas." });

            var result = await _repository.CreateAsync(createFraisConcernerClasseDto);
            if(result == null) return Conflict(new {message ="Cet enregistrement existe déjà"});
            return StatusCode(201, result.ToFraisConcernerClasseDto());
        }

        [HttpPatch("{fraisId}/{classeId}/{anneeScolaireId}")]
        public async Task<IActionResult> Patch(int fraisId, int classeId,int anneeScolaireId,
                 [FromBody] UpdateFraisConcernerClasseDto updateFraisConcernerClasseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repository.UpdateAsync(fraisId,classeId, anneeScolaireId,updateFraisConcernerClasseDto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{fraisId}/{classeId}/{anneeScolaireId}")]
        public async Task<IActionResult> Delete(int fraisId, int classeId, int anneeScolaireId)
        {
            var result =await _repository.DeleteAsync(fraisId,classeId, anneeScolaireId);
            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}