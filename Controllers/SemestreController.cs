using System.Data;

using api_gestion_ecole.Dtos.Semestre;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [ApiController]
    [Route("api/semestres")]
    public class SemestreController : ControllerBase
    {
        private readonly ISemestreRepository _semestreRepository;
        public SemestreController(ISemestreRepository semestreRepository)
        {
            _semestreRepository = semestreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _semestreRepository.GetAllAsync()).Select(p=>p.ToSemestreDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var semestre = await _semestreRepository.GetByIdAsync(id);
            if(semestre == null) return NotFound(new { message = "Semestre introuvable"});
            return Ok(semestre.ToSemestreDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSemestreDto semestreDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var semstre = await _semestreRepository.CreateAsync(semestreDto);
            return StatusCode(201, semstre?.ToSemestreDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSemestreDto updateSemestreDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var semestre = await _semestreRepository.UpdateAsync(id, updateSemestreDto);
            if(semestre == null) return NotFound(new { message = "Semestre introuvable" });
            return Ok(semestre.ToSemestreDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var semestre = await _semestreRepository.DeleteAsync(id);
            if(semestre == null) return NotFound(new { message = "Semestre introuvable"});
            return NoContent();
        }
    }
}