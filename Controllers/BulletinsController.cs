using api_gestion_ecole.Dtos.Bulletins;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [ApiController]
    [Route("api/bulletins")]
    public class BulletinsController : ControllerBase
    {
        private readonly IBulletinsRepository _repository;

        public BulletinsController(IBulletinsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("inscription/{inscriptionId}/periode/{periodeId}")]
        public async Task<ActionResult<BulletinDto>> GetBulletinPeriode(int inscriptionId, int periodeId)
        {
            var bulletin = await _repository.CalculerBulletinPeriodeAsync(inscriptionId, periodeId);
            if (bulletin == null) return NotFound(new { Message = "Données introuvables." });

            return Ok(bulletin);
        }

        [HttpGet("inscription/{inscriptionId}/semestre/{semestreId}")]
        public async Task<ActionResult<BulletinDto>> GetBulletinSemestre(int inscriptionId, int semestreId)
        {
            var bulletin = await _repository.CalculerBulletinSemestreAsync(inscriptionId, semestreId);
            if (bulletin == null) return NotFound(new { Message = "Données introuvables." });

            return Ok(bulletin);
        }
    }
}