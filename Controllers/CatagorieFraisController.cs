using api_gestion_ecole.Dtos.CategorieFrais;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/categoriefrais")]
    [ApiController]
    public class CatagorieFraisController : ControllerBase
    {
        private readonly ICategorieFraisRepository _categorieFraisRepository;
        public CatagorieFraisController(ICategorieFraisRepository categorieFraisRepository)
        {
            _categorieFraisRepository = categorieFraisRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok((await _categorieFraisRepository.GetAllAsync()).Select(c=>c.ToCategorieFraisDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categorieFrais = await _categorieFraisRepository.GetByIdAsync(id);
            if(categorieFrais == null) return NotFound(new {message = "Categorie frais introuvable"});
            return Ok(categorieFrais.ToCategorieFraisDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategorieFraisDto createCategorieFraisDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categorieFrais = await _categorieFraisRepository.CreateAsync(createCategorieFraisDto);
            return StatusCode(201, categorieFrais?.ToCategorieFraisDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategorieFraisDto updateCategorieFraisDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categorieFrais = await _categorieFraisRepository.UpdateAsync(id, updateCategorieFraisDto);
            if(categorieFrais == null) return NotFound(new { message = "Categorie frais introuvable"});
            return Ok(categorieFrais.ToCategorieFraisDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var categorieFrais = await _categorieFraisRepository.DeleteAsync(id);
            if(categorieFrais == null) return NotFound(new {message = "Categorie frais introuvable"});
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreCategories()
        {
            var nombre = await _categorieFraisRepository.GetNombreCategoriesAsync();
            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/nombre-frais")]
        public async Task<IActionResult> GetNombreFrais(int id)
        {
            var nombre = await _categorieFraisRepository.GetNombreFraisAsync(id);

            if (nombre == null)
                return NotFound(new { message = "Catégorie de frais introuvable" });

            return Ok(new { nombre });
        }
    }
}