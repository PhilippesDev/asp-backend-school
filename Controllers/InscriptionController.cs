using api_gestion_ecole.Dtos.Insciption;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/inscriptions")]
    [ApiController]
    public class InscriptionController:ControllerBase
    {
        private readonly IInscriptionRepository _inscriptionRepository;
        public InscriptionController(IInscriptionRepository inscriptionRepository)
        {
            _inscriptionRepository = inscriptionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(QueryObject queryObject)
        {
            return Ok((await _inscriptionRepository.GetAllAsync(queryObject))
                        .Select(i=>i.ToInscriptionDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var insciption = await _inscriptionRepository.GetByIdAsync(id);
            if(insciption == null) 
                return NotFound(new {message = "Elève introuvable ! "});
            return Ok(insciption.ToInscriptionDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateInscriptionDto createInscriptionDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _inscriptionRepository.IsEleveExistAsync(createInscriptionDto.EleveId))
                return BadRequest(new {message = "L'élève spécifié(e) n'existe pas"});

            if(!await _inscriptionRepository.IsClasseExistAsync(createInscriptionDto.ClasseId))
                return BadRequest(new {message = "La classe spécifiée n'existe pas"});
            
            if(!await _inscriptionRepository.IsAnneeScolaireExistAsync(createInscriptionDto.AnneeScolaireId))
                return BadRequest(new {message = "L'année scolaire spécifiée n'existe pas"});

            if(await _inscriptionRepository.IsEleveAlreadyInscritAsync(
                createInscriptionDto.EleveId,
                createInscriptionDto.AnneeScolaireId))
            {
                return BadRequest(new {message = "Cet élève est déjà inscrit"});
            }

            var inscriptionCreated = await _inscriptionRepository.CreateAsync(createInscriptionDto);
            return StatusCode(201, inscriptionCreated?.ToInscriptionDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInscriptionDto updateInscriptionDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            var inscriptionUpdated = await _inscriptionRepository.UpdateAsync(id, updateInscriptionDto);

            if(inscriptionUpdated == null)
                return NotFound(new {message = "Inscription introuvable"});
            
            return Ok(inscriptionUpdated.ToInscriptionDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var inscription = await _inscriptionRepository.DeleteAsync(id);
            if(inscription == null)
                return BadRequest(new {message = "Inscription introuvable"});
            
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreInscriptions()
        {
            var nombre = await _inscriptionRepository.GetNombreInscriptionsAsync(string.Empty);

            if (nombre == null)
                return NotFound(new { message = "Aucune année scolaire active" });

            return Ok(new { nombre });
        }

        [HttpGet("nombre/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetNombreInscriptions(string anneeScolaireDesignation)
        {
            var nombre = await _inscriptionRepository.GetNombreInscriptionsAsync(anneeScolaireDesignation);

            if (nombre == null)
                return NotFound(new { message = "L'année scolaire spécifiée est introuvable" });

            return Ok(new { nombre });
        }
    }
}