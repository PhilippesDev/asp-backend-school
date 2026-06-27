using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Helpers;
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

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreClasse()
        {
            var effectif = await _classeRepository.GetNombreClasseAsync();
            return Ok( new {effectif = effectif});
        }

        [HttpGet("{classeId:int}/effectif")]
        public async Task<IActionResult> GetNombreElevesInClasse(int classeId)
        {
            var effectif = await _classeRepository.GetNombreEleveInClasseAsync(classeId, string.Empty);

            if(effectif == null)
                return NotFound(new {message = "La classe ou l'année scolaire spécifiée est introuvable"});

            return Ok( new {effectif = effectif});
        }

        [HttpGet("{classeId:int}/effectif/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetNombreElevesInClasse(int classeId, string anneeScolaireDesignation)
        {
            var nombre = await _classeRepository.GetNombreEleveInClasseAsync(classeId, anneeScolaireDesignation);

            if(nombre == null)
                return NotFound(new {message = "La classe ou l'année scolaire spécifiée est introuvable"});
                
            return Ok( new {nombre = nombre});
        }

        [HttpGet("{classeId:int}/nombre-cours")]
        public async Task<IActionResult> GetNombreCoursInClasse(int classeId)
        {
            var nombre = await _classeRepository.GetNombreCoursInClasseAsync(classeId, string.Empty);

            if(nombre == null)
                return NotFound(new {message = "La classe ou l'année scolaire spécifiée est introuvable"});

            return Ok( new {nombre = nombre});
        }

        [HttpGet("{classeId:int}/nombre-cours/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetNombreCoursInClasse(int classeId, string anneeScolaireDesignation)
        {
            var nombre = await _classeRepository.GetNombreCoursInClasseAsync(classeId, anneeScolaireDesignation);

            if(nombre == null)
                return NotFound(new {message = "La classe ou l'année scolaire spécifiée est introuvable"});
                
            return Ok( new {nombre = nombre});
        }

        [HttpGet("{classeId:int}/montant-total-frais")]
        public async Task<IActionResult> GetMontantAPayerInClasse(int classeId)
        {
            var montant = await _classeRepository.GetMontantAPayerInClasseAsync(classeId, string.Empty);

            if(montant == null)
                return NotFound(new {message = "La classe ou l'année scolaire spécifiée est introuvable"});

            return Ok( new {montant = montant});
        }

        [HttpGet("{classeId:int}/montant-total-frais/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetMontantAPayerInClasse(int classeId, string anneeScolaireDesignation)
        {
            var montant = await _classeRepository.GetMontantAPayerInClasseAsync(classeId, anneeScolaireDesignation);

            if(montant == null)
                return NotFound(new {message = "La classe ou l'année scolaire spécifiée est introuvable"});
                
            return Ok( new {montant = montant});
        }

        [HttpGet("classes-effectifs")]
        public async Task<IActionResult> GetClasseWithNombreEleve([FromQuery] QueryObject queryObject) 
        {
            var classes = await _classeRepository.GetNombreEleveParClasseAsync(string.Empty, queryObject);

            if(classes == null)
                return NotFound(new {message = "Aucune année scolaire est active, veillez activé ou spécifié une année scolaire"});

            return Ok(classes);
        }

        [HttpGet("classes-effectifs/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetClasseWithNombreEleve(string anneeScolaireDesignation, [FromQuery] QueryObject queryObject) 
        {
            var classes = await _classeRepository.GetNombreEleveParClasseAsync(anneeScolaireDesignation, queryObject);

            if(classes == null)
                return NotFound(new {message = "L'année scolaire spécifiée est introuvable"});

            return Ok(classes);
        }

        [HttpGet("{classeId:int}/cours")]
        public async Task<IActionResult> GetCoursInClasse(int classeId)
        {
            var cours = await _classeRepository.GetCoursInClasseAsync(classeId, string.Empty);

            if (cours == null)
                return NotFound(new { message = "La classe ou l'année scolaire spécifiée est introuvable" });

            return Ok(cours.Select(c => c.ToCoursConcernerClasseDto()));
        }

        [HttpGet("{classeId:int}/cours/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetCoursInClasse(int classeId, string anneeScolaireDesignation)
        {
            var cours = await _classeRepository.GetCoursInClasseAsync(classeId, anneeScolaireDesignation);

            if (cours == null)
                return NotFound(new { message = "La classe ou l'année scolaire spécifiée est introuvable" });

            return Ok(cours.Select(c => c.ToCoursConcernerClasseDto()));
        }

        [HttpGet("{classeId:int}/frais")]
        public async Task<IActionResult> GetFraisInClasse(int classeId)
        {
            var frais = await _classeRepository.GetFraisInClasseAsync(classeId, string.Empty);

            if (frais == null)
                return NotFound(new { message = "La classe ou l'année scolaire spécifiée est introuvable" });

            return Ok(frais.Select(f => f.ToFraisConcernerClasseDto()));
        }

        [HttpGet("{classeId:int}/frais/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetFraisInClasse(int classeId, string anneeScolaireDesignation)
        {
            var frais = await _classeRepository.GetFraisInClasseAsync(classeId, anneeScolaireDesignation);

            if (frais == null)
                return NotFound(new { message = "La classe ou l'année scolaire spécifiée est introuvable" });

            return Ok(frais.Select(f => f.ToFraisConcernerClasseDto()));
        }

        [HttpGet("classes-montants-frais")]
        public async Task<IActionResult> GetClasseWithMontantFrais([FromQuery] QueryObject queryObject)
        {
            var classes = await _classeRepository.GetMontantFraisParClasseAsync(string.Empty, queryObject);

            if (classes == null)
                return NotFound(new { message = "Aucune année scolaire est active, veillez activé ou spécifié une année scolaire" });

            return Ok(classes);
        }

        [HttpGet("classes-montants-frais/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetClasseWithMontantFrais(string anneeScolaireDesignation, [FromQuery] QueryObject queryObject)
        {
            var classes = await _classeRepository.GetMontantFraisParClasseAsync(anneeScolaireDesignation, queryObject);

            if (classes == null)
                return NotFound(new { message = "L'année scolaire spécifiée est introuvable" });

            return Ok(classes);
        }
    }
}