using api_gestion_ecole.Dtos.Option_;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/options")]
    [ApiController]
    public class OptionController: ControllerBase
    {
        private readonly IOptionRepository _optionRepository;
        public OptionController(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _optionRepository.GetAllAsync()).Select(o=>o.ToOptionDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var option = await _optionRepository.GetByIdAsync(id);
            if(option == null) return NotFound(new { message = "Option introuvable"});
            return Ok(option.ToOptionDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOptionDto createOptionDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var option = await _optionRepository.CreateAsync(createOptionDto);
            return StatusCode(201, option?.ToOptionDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOptionDto updateOptionDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var option = await _optionRepository.UpdateAsync(id, updateOptionDto);
            if(option == null) return NotFound(new { message = "Option introuvable" });
            return Ok(option.ToOptionDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var option = await _optionRepository.DeleteAsync(id);
            if(option == null) return NotFound(new { message = "Option introuvable"});
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreOptions()
        {
            var nombre = await _optionRepository.GetNombreOptionsAsync();
            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/nombre-classes")]
        public async Task<IActionResult> GetNombreClasses(int id)
        {
            var nombre = await _optionRepository.GetNombreClassesAsync(id);

            if (nombre == null)
                return NotFound(new { message = "Option introuvable" });

            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/effectif")]
        public async Task<IActionResult> GetEffectif(int id)
        {
            var effectif = await _optionRepository.GetEffectifAsync(id, string.Empty);

            if (effectif == null)
                return NotFound(new { message = "L'option ou l'année scolaire spécifiée est introuvable" });

            return Ok(new { effectif });
        }

        [HttpGet("{id:int}/effectif/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetEffectif(int id, string anneeScolaireDesignation)
        {
            var effectif = await _optionRepository.GetEffectifAsync(id, anneeScolaireDesignation);

            if (effectif == null)
                return NotFound(new { message = "L'option ou l'année scolaire spécifiée est introuvable" });

            return Ok(new { effectif });
        }

        [HttpGet("options-effectifs")]
        public async Task<IActionResult> GetEffectifParOption([FromQuery] QueryObject queryObject)
        {
            var options = await _optionRepository.GetEffectifParOptionAsync(string.Empty, queryObject);

            if (options == null)
                return NotFound(new { message = "Aucune année scolaire est active" });

            return Ok(options);
        }

        [HttpGet("options-effectifs/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetEffectifParOption(string anneeScolaireDesignation, [FromQuery] QueryObject queryObject)
        {
            var options = await _optionRepository.GetEffectifParOptionAsync(anneeScolaireDesignation, queryObject);

            if (options == null)
                return NotFound(new { message = "L'année scolaire spécifiée est introuvable" });

            return Ok(options);
        }
    }
}