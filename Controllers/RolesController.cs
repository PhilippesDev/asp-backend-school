using api_gestion_ecole.Dtos.Roles;
using api_gestion_ecole.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/auth/roles")]
    [ApiController]
    public class RolesController:ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _rolesRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _rolesRepository.GetByIdAsync(id);
            if(role == null) return NotFound(new { message = "Rôle introuvable"});
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRoleDto createRoleDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if(await _rolesRepository.IsRoleExistAsync(createRoleDto.Name))
                return Conflict(new {message = "Ce role existe déjà"});

            var role = await _rolesRepository.CreateAsync(createRoleDto);
            if(role == null) return StatusCode(500, new {message = "Une erreur s'est produite lors de la création d'un nouveau rôle"});

            return Ok(role);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var role = await _rolesRepository.UpdateAsync(id, updateRoleDto);
            if(role == null) return NotFound(new { message = "Role introuvable" });
            return Ok(role);
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> Delete(string roleName)
        {
            var periode = await _rolesRepository.DeleteAsync(roleName);
            if(periode == null) return NotFound(new { message = "Role introuvable"});
            return NoContent();
        }
    }
}