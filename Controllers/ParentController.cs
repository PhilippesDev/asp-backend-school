using api_gestion_ecole.Dtos.Classe;
using api_gestion_ecole.Dtos.Eleve;
using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Dtos.Parent;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/parents")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentRepository _parentRepository;
    
        public ParentController(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _parentRepository.GetAllAsync()).Select(e=>e.ToParentDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var parent = await _parentRepository.GetByIdAsync(id);
            if(parent == null) return NotFound(new { message = "Parent introuvable"});
            return Ok(parent.ToParentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateParentDto createParentDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            
            if(await _parentRepository.NumeroParentExistAsync(createParentDto.Telephone))
                return Conflict(new {message = "Le parent associé à ce numéro existe déjà !"});

            var parent = await _parentRepository.CreateAsync(createParentDto);
            return StatusCode(201, parent?.ToParentDto());
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateParentDto updateParentDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var parent = await _parentRepository.UpdateAsync(id, updateParentDto);
            if(parent == null) return NotFound(new { message = "Parent introuvable" });
            return Ok(parent.ToParentDto());
        }
    }
}