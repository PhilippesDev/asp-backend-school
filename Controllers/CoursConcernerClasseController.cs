using api_gestion_ecole.Dtos.CoursConcernerClasse;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/cours-concerner-classes")]
    [ApiController]
    public class CoursConcernerClasseController : ControllerBase
    {
        private readonly ICoursConcernerClasseRepository _repository;
        public CoursConcernerClasseController(ICoursConcernerClasseRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var coursConcernerClasses = await _repository.GetAllAsync();
            return Ok(coursConcernerClasses.Select(c=>
                        c.ToCoursConcernerClasseDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coursConcernerClasse = await _repository.GetByIdAsync(id);

            if (coursConcernerClasse == null)
                return NotFound();

            return Ok(coursConcernerClasse.ToCoursConcernerClasseDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCoursConcernerClasseDto createCoursConcernerClasseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coursExists =
                await _repository.IsCoursExistAsync(createCoursConcernerClasseDto.CoursId);

            if (!coursExists)
                return BadRequest(new {message ="Le cours spécifié n'existe pas."});

            var classeExists =
                await _repository.IsClasseExistAsync(createCoursConcernerClasseDto.ClasseId);

            if (!classeExists)
                return BadRequest(new {message ="La classe spécifiée n'existe pas."});
            
            var enseignantExists =
                await _repository.IsClasseExistAsync(createCoursConcernerClasseDto.EnseignantId);

            if (!enseignantExists)
                return BadRequest(new {message ="L'enseignant spécifié n'existe pas."});

            var anneeScolaireExists =
                await _repository.IsAnneeScolaireExistAsync(createCoursConcernerClasseDto.AnneeScolaireId);

            if (!anneeScolaireExists)
                return BadRequest(new {message = "L'année scolaire spécifiée n'existe pas." });


            var result = await _repository.CreateAsync(createCoursConcernerClasseDto);
            if(result == null) return Conflict(new {message ="Cet enregistrement existe déjà"});
            return StatusCode(201,result.ToCoursConcernerClasseDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id,
                 [FromBody] UpdateCoursConcernerClasseDto updateCoursConcernerClasseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repository.IsEnseignantExistAsync(updateCoursConcernerClasseDto.EnseignantId))
                return BadRequest(new {message ="L'enseignant spécifié n'existe pas."});
                
            var result = await _repository.UpdateAsync(id,updateCoursConcernerClasseDto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =await _repository.DeleteAsync(id);
            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}