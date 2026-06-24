using api_gestion_ecole.Dtos.Cours;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/cours")]
    [ApiController]
    public class CoursController: ControllerBase
    {
        private readonly ICoursRepository _coursRepository;
        public CoursController(ICoursRepository coursRepository)
        {
            _coursRepository = coursRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(QueryObject queryObject)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _coursRepository.GetAllAsync(queryObject)).Select(c=>c.ToCoursDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var cours = await _coursRepository.GetByIdAsync(id);
            if(cours == null) return NotFound(new { message = "Cours introuvable"});
            return Ok(cours.ToCoursDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCoursDto createCoursDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var cours = await _coursRepository.CreateAsync(createCoursDto);
            return StatusCode(201, cours?.ToCoursDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCoursDto updateCoursDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var cours = await _coursRepository.UpdateAsync(id, updateCoursDto);
            if(cours == null) return NotFound(new { message = "Cours introuvable" });
            return Ok(cours.ToCoursDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var cours = await _coursRepository.DeleteAsync(id);
            if(cours == null) return NotFound(new { message = "Cours introuvable"});
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreCours()
        {
            var nombre = await _coursRepository.GetNombreCoursAsync();
            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/nombre-classes")]
        public async Task<IActionResult> GetNombreClassesConcernees(int id)
        {
            var nombre = await _coursRepository.GetNombreClassesConcerneesAsync(id, string.Empty);

            if (nombre == null)
                return NotFound(new { message = "Le cours ou l'année scolaire spécifiée est introuvable" });

            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/nombre-classes/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetNombreClassesConcernees(int id, string anneeScolaireDesignation)
        {
            var nombre = await _coursRepository.GetNombreClassesConcerneesAsync(id, anneeScolaireDesignation);

            if (nombre == null)
                return NotFound(new { message = "Le cours ou l'année scolaire spécifiée est introuvable" });

            return Ok(new { nombre });
        }

        [HttpGet("classes/{classeId}")]
        public async Task<IActionResult> GetListeCoursPourClasse(int classeId)
        {
            var cours = await _coursRepository.GetListedeCoursPourClasseAsync(classeId, string.Empty);

            if (cours == null)
                return NotFound(new { message = "La classe ou l'année scolaire spécifiée est introuvable" });

            return Ok(cours.Select(c=>c.ToCoursConcernerClasseDto()));
        }

        [HttpGet("classes/{classeId}/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetListeCoursPourClasse(int classeId, string anneeScolaireDesignation)
        {
            var cours = await _coursRepository.GetListedeCoursPourClasseAsync(classeId, anneeScolaireDesignation);

            if (cours == null)
                return NotFound(new { message = "La classe ou l'année scolaire spécifiée est introuvable" });

            return Ok(cours.Select(c=>c.ToCoursConcernerClasseDto()));
        }
    }
}