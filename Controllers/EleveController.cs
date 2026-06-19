using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/eleves")]
    [ApiController]
    public class EleveController : ControllerBase
    {
        private readonly IEleveRepository _eleveRepository;
        public EleveController(IEleveRepository eleveRepository)
        {
            _eleveRepository = eleveRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok((await _eleveRepository.GetAllAsync()).Select(e=>e.ToEleveDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var eleve = await _eleveRepository.GetByIdAsync(id);
            if(eleve == null) return NotFound(new { message = "Elève introuvable"});
            return Ok(eleve.ToEleveDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEleveDto createEleveDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var eleve = await _eleveRepository.CreateAsync(createEleveDto);
            return StatusCode(201, eleve?.ToEleveDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEleveDto updateEleveDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var eleve = await _eleveRepository.UpdateAsync(id, updateEleveDto);
            if(eleve == null) return NotFound(new { message = "Elève introuvable" });
            return Ok(eleve.ToEleveDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var eleve = await _eleveRepository.DeleteAsync(id);
            if(eleve == null) return NotFound(new { message = "Elève introuvable"});
            return NoContent();
        }
    }
}