using api_gestion_ecole.Dtos.Enseignant;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [ApiController]
    [Route("api/enseignants")]
    public class EnseignantController : ControllerBase
    {
        private readonly IEnseignantRepository _enseignantRepository;
        private readonly string[] EXTENSIONS_AUTORISEES = [".jpg", ".jpeg", ".png"];
        private const long TAILLE_MAXIMALE_OCTETS = 2 * 1024 * 1024;
        private readonly IWebHostEnvironment _env;
        private readonly UploadImageService _uploadImageService;

        public EnseignantController(IEnseignantRepository enseignantRepository, IWebHostEnvironment env, UploadImageService uploadImageService)
        {
            _enseignantRepository = enseignantRepository;
            _env = env;
            _uploadImageService = uploadImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(QueryObjectForPeople queryObject)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok((await _enseignantRepository.GetAllAsync(queryObject)).Select(e=>e.ToEnseignantDto()));
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var enseignant = await _enseignantRepository.GetByIdAsync(id);
            if(enseignant == null) return NotFound(new { message = "Enseignant introuvable"});
            return Ok(enseignant.ToEnseignantDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateEnseignantDto createEnseignantDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (createEnseignantDto.ImageFile != null)
            {
                var messageErreur = ValiderImage(createEnseignantDto.ImageFile);
                if (messageErreur != null) return BadRequest(new { message = messageErreur });

                createEnseignantDto.Photo = await _uploadImageService.SauvegarderImageAsync(createEnseignantDto.ImageFile, "enseignants");
            }

            var enseignant = await _enseignantRepository.CreateAsync(createEnseignantDto);
            return StatusCode(201, enseignant?.ToEnseignantDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateEnseignantDto updateEnseignantDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (updateEnseignantDto.ImageFile != null)
            {
                var messageErreur = ValiderImage(updateEnseignantDto.ImageFile);
                if (messageErreur != null) return BadRequest(new { message = messageErreur });

                _uploadImageService.SupprimerImagePhysiqueAsync(updateEnseignantDto.Photo, "enseignants");
                updateEnseignantDto.Photo = await _uploadImageService.SauvegarderImageAsync(updateEnseignantDto.ImageFile, "enseignants");
            }

            var enseignant = await _enseignantRepository.UpdateAsync(id, updateEnseignantDto);
            if(enseignant == null) return NotFound(new { message = "Enseignant introuvable" });
            return Ok(enseignant.ToEnseignantDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var enseignant = await _enseignantRepository.DeleteAsync(id);
            if(enseignant == null) return NotFound(new { message = "Enseignant introuvable"});

            _uploadImageService.SupprimerImagePhysiqueAsync(enseignant.Photo, "enseignants");
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreEleves()
        {
            var nombre = await _enseignantRepository.GetNombreEnseignatsAsync();
            return Ok(new { nombre });
        }

        private string? ValiderImage(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!EXTENSIONS_AUTORISEES.Contains(extension))
            {
                return $"Format non supporté. Extensions autorisées : {string.Join(", ", EXTENSIONS_AUTORISEES)}";
            }

            if (file.Length > TAILLE_MAXIMALE_OCTETS)
            {
                return "Le fichier est trop lourd. La taille maximale est de 2 Mo.";
            }

            return null;
        }
    }
}