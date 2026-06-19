using api_gestion_ecole.Dtos.Option_;
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
    }
}