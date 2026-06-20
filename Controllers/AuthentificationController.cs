using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/auth/users")]
    [ApiController]
    public class AuthentificationController:ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        } 

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        } 
    
        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            return Ok();
        } 

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return NoContent();
        }
    }
}