using api_gestion_ecole.Dtos.Cotation;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api_gestion_ecole.Controllers
{
    [Route("api/cotations")]
    [ApiController]
    public class CotationController:ControllerBase
    {
        private readonly ICotationRepository _repository;
        public CotationController(ICotationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _repository.GetAllAsync())
                    .Select(c=>c.ToCotationDto()));
        }

        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetById(int id)
        {
            var cotation = await _repository.GetByIdAsync(id);
            if(cotation == null)
                return NotFound(new {message = "Cotation introuvable"});
            return Ok(cotation.ToCotationDto());
        }

        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateCotationDto createCotationDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(await _repository.IsCotationExistAsync(createCotationDto.InscriptionId,
            createCotationDto.CoursConcernerClasseId, 
            createCotationDto.PeriodeId))
            {
                return Conflict(new {message = "Cet élève a déjà été coté"});
            }
            
            if(!await _repository.IsInscriptionExistAsync(createCotationDto.InscriptionId))
                return BadRequest(new {message = "L'inscription spécifiée est introuvable"});

            if(!await _repository.IsCoursConcernerClasseExistAsync(createCotationDto.CoursConcernerClasseId))
                return BadRequest(new {message = "Le cours spécifié est introuvable"});

            if(!await _repository.IsPeriodeExistAsync(createCotationDto.PeriodeId))
                return BadRequest(new {message = "La période spécifiée est introuvable"});

            var cotation = await _repository.CreateAsync(createCotationDto);
            if(cotation == null)
                return BadRequest(new {message = "Erreur de cotation ! Verifiez si le cours concerne l'élève " + 
                        " ou si la cote n'est pas supérieur au maximum"});

            return StatusCode(201, cotation.ToCotationDto());
        }

        [HttpPatch("{id:int}")] 
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCotationDto updateCotationDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var checkCoteErrorMessage = await _repository.ErrorMessageCheckCoteWhenUpdating(id, updateCotationDto.Cote);
            
            if(checkCoteErrorMessage != null && checkCoteErrorMessage != "not found")
                return BadRequest(new {message = checkCoteErrorMessage});

            var cotation = await _repository.UpdateAsync(id, updateCotationDto);
            if(cotation == null) return NotFound(new {message = "Cotation introuvale"});

            return Ok(cotation.ToCotationDto());
        }

        [HttpDelete("{id:int}")] 
        public async Task<IActionResult> Delete(int id)
        {
            var cotation = await _repository.DeleteAsync(id);
            if(cotation == null) return NotFound(new {message = "Cotation introuvable"});
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreCotations()
        {
            var nombre = await _repository.GetNombreCotationsAsync();
            return Ok(new { nombre });
        }
    }
}