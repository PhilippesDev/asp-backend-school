using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Dtos.Presence;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/presences")]
    public class PresenceController:ControllerBase
    {
        private readonly IPresenceRepository _repository;
        public PresenceController(IPresenceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _repository.GetAllAsync())
                .Select(p=>p.ToPresenceDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var presence = await _repository.GetByIdAsync(id);
            if(presence == null) return NotFound(new {message  = "Presence introuvable"});
            return Ok(presence.ToPresenceDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePresenceDto createPresenceDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(!await _repository.IsInscriptionExist(createPresenceDto.InscriptionId))
                return BadRequest(new {message = "Cet élève n'existe pas ou n'a pas encore été inscrit dans cette année scolaire"});

            var presence = await _repository.CreateAsync(createPresenceDto);
            return StatusCode(201, presence?.ToPresenceDto());
        }

        [HttpGet("nombre-eleves-presents")]
        public async Task<IActionResult> GetNombreElevePresents()
        {
            int nombre = await _repository.GetNombreELevesPresents();
            return Ok(new {nombre});
        }

        [HttpGet("nombre-eleves-absents/{anneeScolaireId:int}")]
        public async Task<IActionResult> GetNombreEleveAbsent(int anneeScolaireId)
        {
            int nombre = await _repository.GetNombreELevesAbsents(anneeScolaireId);
            return Ok(new {nombre});
        }

        [HttpGet("classes/{classeId:int}/nombre-eleves-presents")]
        public async Task<IActionResult> GetNombreElevePresentsInClasse(int classeId)
        {
            int nombre = await _repository.GetNombreELevesPresentsInClasse(classeId);
            return Ok(new {nombre});
        }

        [HttpGet("classes/{classeId:int}/anneesScolaires/{anneeScolaireId}/-eleves-absents")]
        public async Task<IActionResult> GetNombreEleveAbsentInClasse(int classeId,  int anneeScolaireId)
        {
            int nombre = await _repository.GetNombreELevesAbsentsInClasse(anneeScolaireId, anneeScolaireId);
            return Ok(new {nombre});
        }
    }
}