using api_gestion_ecole.Dtos.Periode;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/periodes")]
    [ApiController]
    public class PeriodeController: ControllerBase
    {
        private readonly IPeriodeRepository _periodeRepository;
        public PeriodeController(IPeriodeRepository periodeRepository)
        {
            _periodeRepository = periodeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _periodeRepository.GetAllAsync()).Select(p=>p.ToPeriodeDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var periode = await _periodeRepository.GetByIdAsync(id);
            if(periode == null) return NotFound(new { message = "Periode introuvable"});
            return Ok(periode.ToPeriodeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePeriodeDto createPeriodeDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _periodeRepository.IsSemestreExistAsync(createPeriodeDto.SemestreId))
                return BadRequest(new {message = "Le semestre spécifié est introuvable"});

            var periode = await _periodeRepository.CreateAsync(createPeriodeDto);
            return StatusCode(201, periode?.ToPeriodeDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePeriodeDto updatePeriodeDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

             if(!await _periodeRepository.IsSemestreExistAsync(updatePeriodeDto.SemestreId))
                return BadRequest(new {message = "Le semestre spécifié est introuvable"});

            var periode = await _periodeRepository.UpdateAsync(id, updatePeriodeDto);
            if(periode == null) return NotFound(new { message = "Periode introuvable" });
            return Ok(periode.ToPeriodeDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var periode = await _periodeRepository.DeleteAsync(id);
            if(periode == null) return NotFound(new { message = "Periode introuvable"});
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombrePeriodes()
        {
            var nombre = await _periodeRepository.GetNombrePeriodesAsync();
            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/nombre-cotations")]
        public async Task<IActionResult> GetNombreCotations(int id)
        {
            var nombre = await _periodeRepository.GetNombreCotationsAsync(id);

            if (nombre == null)
                return NotFound(new { message = "Période introuvable" });

            return Ok(new { nombre });
        }
    }
}