using api_gestion_ecole.Dtos.Auth;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IUserRepository _repository;
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObjectForPeople queryObject)
        {
            return Ok((await _repository.GetAllAsync(queryObject)).Select(u=>u.ToUserDto()));
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _repository.GetByIdAsync(id);
            if(user == null) return NotFound(new {message  = "Utilisateur introuvable"});
            return Ok(user.ToUserDto());
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Post([FromBody] RegistorUserDto registorUserDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
        
            if(await _repository.IsEmailExistAsync(registorUserDto.Email))
                return BadRequest(new {message = "L'utilisateur associé à cette adresse mail existe déjà"});

            var user = await _repository.CreateAsync(registorUserDto);

            if(user == null) return StatusCode(500, new {message = "Une erreur s'est produit lors de la création du compte"});

            return StatusCode(201, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _repository.LoginAsync(loginDto);
            if(user == null) 
                return Unauthorized(new {message = "Le nom ou le mot de passe est incorrect"});
            
            return Ok(user);
        }
    }
}