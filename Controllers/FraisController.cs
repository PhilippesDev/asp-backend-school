using api_gestion_ecole.Dtos.Frais;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/frais")]
    [ApiController]
    public class FraisController: ControllerBase
    {
        private readonly IFraisRepository _fraisRepository;
        public FraisController(IFraisRepository fraisRepository)
        {
            _fraisRepository = fraisRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _fraisRepository.GetAllAsync()).Select(f=>f.ToFraisDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var frais = await _fraisRepository.GetByIdAsync(id);
            if(frais == null) return NotFound(new { message = "Frais introuvable"});
            return Ok(frais.ToFraisDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateFraisDto createFraisDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _fraisRepository.IsCategorieFraisExistAsync(createFraisDto.CategorieFraisId))
            {
                return BadRequest(new {message = "Veillez entrer une categorie de frais existante"});
            }

            var frais = await _fraisRepository.CreateAsync(createFraisDto);
            return StatusCode(201, frais?.ToFraisDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFraisDto updateFraisDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _fraisRepository.IsCategorieFraisExistAsync(updateFraisDto.CategorieFraisId))
            {
                return BadRequest(new {message = "Veillez entrer une categorie de frais existante"});
            }

            var frais = await _fraisRepository.UpdateAsync(id, updateFraisDto);
            if(frais == null) return NotFound(new { message = "Frais introuvable" });
            return Ok(frais.ToFraisDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var frais = await _fraisRepository.DeleteAsync(id);
            if(frais == null) return NotFound(new { message = "Frais introuvable"});
            return NoContent();
        }
    }
}