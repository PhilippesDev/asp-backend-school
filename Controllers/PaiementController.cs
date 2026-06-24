using api_gestion_ecole.Dtos.Paiement;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api_gestion_ecole.Controllers
{
    [Route("api/paiements")]
    public class PaiementController:ControllerBase
    {
        private readonly IPaiementRepository _repository;
        public PaiementController(IPaiementRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _repository.GetAllAsync())
                .Select(p=>p.ToPaiementDto()));
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paiement = await _repository.GetByIdAsync(id);
            if(paiement == null) return NotFound(new {message  = "Paiement introuvable"});
            return Ok(paiement.ToPaiementDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePaiementDto createPaiementDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(!await _repository.IsInscriptionExistAsync(createPaiementDto.InscriptionId))
                return BadRequest(new {message = "L'élève spécifié n'existe pas"});

             if(!await _repository.IsFraisConcernerClasseExistAsync(createPaiementDto.FraisConcernerClasseId))
                return BadRequest(new {message = "Les frais spécifiés n'existent pas"});
            
            var paiement = await _repository.CreateAsync(createPaiementDto);
            if(paiement == null)
                return BadRequest(new {message = "Erreur d'enregistrement de paiement." +
                                       " Assurez vous que ce frais concerne cet élève"}
                                        );

            return StatusCode(201, paiement?.ToPaiementDto());
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] UpdatePaiementDto updatePaiementDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var paiement = await _repository.UpdateAsync(id, updatePaiementDto);
            if(paiement == null)
                return NotFound(new {message = "Ce paiement n'existe pas"});

            return Ok(paiement.ToPaiementDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paiement = await _repository.DeleteAsync(id);
            if(paiement == null) 
                return NotFound(new {message = "Ce paiement n'existe pas"});
            return NoContent();
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> GetNombrePaiements()
        {
            var nombre = await _repository.GetNombrePaiementsAsync(string.Empty);

            if (nombre == null)
                return NotFound(new { message = "Aucune année scolaire active" });

            return Ok(new { nombre });
        }

        [HttpGet("nombre/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetNombrePaiements(string anneeScolaireDesignation)
        {
            var nombre = await _repository.GetNombrePaiementsAsync(anneeScolaireDesignation);

            if (nombre == null)
                return NotFound(new { message = "L'année scolaire spécifiée est introuvable" });

            return Ok(new { nombre });
        }

        [HttpGet("montant-total")]
        public async Task<IActionResult> GetMontantTotalPaye()
        {
            var montant = await _repository.GetMontantTotalPayeAsync(string.Empty);

            if (montant == null)
                return NotFound(new { message = "Aucune année scolaire active" });

            return Ok(new { montant });
        }

        [HttpGet("montant-total/{anneeScolaireDesignation}")]
        public async Task<IActionResult> GetMontantTotalPaye(string anneeScolaireDesignation)
        {
            var montant = await _repository.GetMontantTotalPayeAsync(anneeScolaireDesignation);

            if (montant == null)
                return NotFound(new { message = "L'année scolaire spécifiée est introuvable" });

            return Ok(new { montant });
        }

        [HttpGet("inscription/{inscriptionId:int}/montant-paye")]
        public async Task<IActionResult> GetMontantPayeByInscription(int inscriptionId)
        {
            var montant = await _repository.GetMontantPayeByInscriptionAsync(inscriptionId);

            if (montant == null)
                return NotFound(new { message = "Inscription introuvable" });

            return Ok(new { montant });
        }

        [HttpGet("inscription/{inscriptionId:int}/montant-restant")]
        public async Task<IActionResult> GetMontantRestantAPayer(int inscriptionId)
        {
            var montant = await _repository.GetMontantRestantAsync(inscriptionId);

            if (montant == null)
                return NotFound(new { message = "Inscription introuvable" });

            return Ok(new { montant });
        }
    }
}