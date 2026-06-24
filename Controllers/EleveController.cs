using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/eleves")]
    [ApiController]
    public class EleveController : ControllerBase
    {
        private readonly IEleveRepository _eleveRepository;
        private readonly string[] EXTENSIONS_AUTORISEES = [".jpg", ".jpeg", ".png"];
        private const long TAILLE_MAXIMALE_OCTETS = 2 * 1024 * 1024;
        private readonly IWebHostEnvironment _env;
        private readonly UploadImageService _uploadImageService;
        public EleveController(IEleveRepository eleveRepository, IWebHostEnvironment env, UploadImageService uploadImageService)
        {
            _eleveRepository = eleveRepository;
            _env = env;
            _uploadImageService = uploadImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            return Ok((await _eleveRepository.GetAllAsync()).Select(e=>e.ToEleveDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var eleve = await _eleveRepository.GetByIdAsync(id);
            if(eleve == null) return NotFound(new { message = "Elève introuvable"});
            return Ok(eleve.ToEleveDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateEleveDto createEleveDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (createEleveDto.ImageFile != null)
            {
                var messageErreur = ValiderImage(createEleveDto.ImageFile);
                if (messageErreur != null) return BadRequest(new { message = messageErreur });

                createEleveDto.Photo = await _uploadImageService.SauvegarderImageAsync(createEleveDto.ImageFile, "eleves");
            }

            var eleve = await _eleveRepository.CreateAsync(createEleveDto);
            return StatusCode(201, eleve?.ToEleveDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateEleveDto updateEleveDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (updateEleveDto.ImageFile != null)
            {
                var messageErreur = ValiderImage(updateEleveDto.ImageFile);
                if (messageErreur != null) return BadRequest(new { message = messageErreur });

                _uploadImageService.SupprimerImagePhysiqueAsync(updateEleveDto.Photo, "eleves");
                updateEleveDto.Photo = await _uploadImageService.SauvegarderImageAsync(updateEleveDto.ImageFile, "eleves");
            }

            var eleve = await _eleveRepository.UpdateAsync(id, updateEleveDto);
            if(eleve == null) return NotFound(new { message = "Elève introuvable" });
            return Ok(eleve.ToEleveDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var eleve = await _eleveRepository.DeleteAsync(id);
            if(eleve == null) return NotFound(new { message = "Elève introuvable"});

            _uploadImageService.SupprimerImagePhysiqueAsync(eleve.Photo, "eleves");
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombreEleves()
        {
            var nombre = await _eleveRepository.GetNombreElevesAsync();
            return Ok(new { nombre });
        }

        [HttpGet("{id:int}/nombre-inscriptions")]
        public async Task<IActionResult> GetNombreInscriptions(int id)
        {
            var nombre = await _eleveRepository.GetNombreInscriptionsAsync(id);

            if (nombre == null)
                return NotFound(new { message = "Elève introuvable" });

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