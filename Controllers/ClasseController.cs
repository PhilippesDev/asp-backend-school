using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClasseController: ControllerBase
    {
        private readonly IClasseRepository _classeRepository;
        public ClasseController(IClasseRepository classeRepository)
        {
            _classeRepository = classeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _classeRepository.GetAllAsync()).Select(c=>c.ToClasseDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
    
            var classe = await _classeRepository.GetByIdAsync(id);
            if(classe == null) return NotFound(new { message = "Classe introuvable"});
            return Ok(classe.ToClasseDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateClasseDto createClasseDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if((await _classeRepository.IsOptionExitAsync(createClasseDto.OptionId)) == false)
            {
                return BadRequest(new {message = "Veillez entrer une option existante"});
            }

            var classe = await _classeRepository.CreateAsync(createClasseDto);
            return StatusCode(201, classe?.ToClasseDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClasseDto updateClasseDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _classeRepository.IsOptionExitAsync(updateClasseDto.OptionId))
            {
                return BadRequest(new {message = "Veillez entrer une option existante"});
            }

            var classe = await _classeRepository.UpdateAsync(id, updateClasseDto);
            if(classe == null) return NotFound(new { message = "Classe introuvable" });
            return Ok(classe.ToClasseDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var classe = await _classeRepository.DeleteAsync(id);
            if(classe == null) return NotFound(new { message = "Classe introuvable"});
            return NoContent();
        }
    }
}