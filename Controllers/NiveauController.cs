using api_gestion_ecole.Dtos.Niveau;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
   
    [ApiController]
    [Route("api/niveaux")]
    public class NiveauController: ControllerBase
    {
        private readonly INiveauRepository _niveauRepository;
        public NiveauController(INiveauRepository niveauRepository)
        {
            _niveauRepository = niveauRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _niveauRepository.GetAllAsync()).Select(n=>n.ToNiveauDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var niveau = await _niveauRepository.GetByIdAsync(id);
            if(niveau == null) return NotFound(new { message = "Niveau introuvable"});
            return Ok(niveau.ToNiveauDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNiveauDto createNiveauDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var niveau = await _niveauRepository.CreateAsync(createNiveauDto);
            return StatusCode(201, niveau?.ToNiveauDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateNiveauDto updateNiveauDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var niveau = await _niveauRepository.UpdateAsync(id, updateNiveauDto);
            if(niveau == null) return NotFound(new { message = "Niveau introuvable" });
            return Ok(niveau.ToNiveauDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var niveau = await _niveauRepository.DeleteAsync(id);
            if(niveau == null) return NotFound(new { message = "Niveau introuvable"});
            return NoContent();
        }
    }
}