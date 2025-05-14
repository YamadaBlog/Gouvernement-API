using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukuna.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvenementController : ControllerBase
    {
        private readonly IEvenementService _evenementService;
        private readonly IMapper _mapper;

        public EvenementController(IEvenementService evenementService, IMapper mapper)
        {
            _evenementService = evenementService;
            _mapper = mapper;
        }

        // GET api/Evenement
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evts = await _evenementService.GetValidatedEvenementsAsync();
            var res = _mapper.Map<IEnumerable<EvenementResource>>(evts);
            return Ok(res);
        }

        // GET api/Evenement/pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var evts = await _evenementService.GetAllEvenementsAsync();
            var res = _mapper.Map<IEnumerable<EvenementResource>>(evts);
            return Ok(res);
        }

        [HttpPut("{id}/validate/{moderateurId}")]
        public async Task<IActionResult> Validate(int id, int moderateurId)
        {
            try
            {
                await _evenementService.ValidateEvenementAsync(id, moderateurId);
                return Ok(new { message = "Événement validé avec succès." });
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ioe.Message);
            }
            catch (DbUpdateException dbu)
            {
                // ⚠️ Affiche la vraie erreur SQL durant le dev
                return StatusCode(500, dbu.InnerException?.Message ?? dbu.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvenement(int id)
        {
            var evenement = await _evenementService.GetEvenementByIdAsync(id);
            if (evenement == null)
                return NotFound();
            var evenementResource = _mapper.Map<Evenement, EvenementResource>(evenement);
            return Ok(evenementResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvenement([FromBody] EvenementResource evenementResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var evenement = _mapper.Map<EvenementResource, Evenement>(evenementResource);
            await _evenementService.CreateEvenementAsync(evenement);
            if (await _evenementService.SaveAsync())
            {
                var result = _mapper.Map<Evenement, EvenementResource>(evenement);
                return CreatedAtAction(nameof(GetEvenement), new { id = evenement.IdEvenement }, result);
            }
            return BadRequest("Erreur lors de la création de l'événement");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvenement(int id, [FromBody] EvenementResource evenementResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var evenementFromDb = await _evenementService.GetEvenementByIdAsync(id);
            if (evenementFromDb == null)
                return NotFound();

            _mapper.Map(evenementResource, evenementFromDb);

            await _evenementService.UpdateEvenementAsync(evenementFromDb);
            if (await _evenementService.SaveAsync())
                return NoContent();

            return Ok("L'évènement est mise à jour");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvenement(int id)
        {
            var evenement = await _evenementService.GetEvenementByIdAsync(id);
            if (evenement == null)
                return NotFound();
            await _evenementService.DeleteEvenementAsync(id);
            if (await _evenementService.SaveAsync())
                return NoContent();
            return BadRequest("Erreur lors de la suppression de l'événement");
        }


        [HttpPost("{id}/demander-modification")]
        public async Task<IActionResult> DemanderModification(int id)
        {
            var evenement = await _evenementService.GetEvenementByIdAsync(id);
            if (evenement == null)
                return NotFound("Événement introuvable.");

            if (evenement.Etat != EtatEvenement.Valide)
                return BadRequest("La demande de modification est réservée aux événements validés.");

            evenement.Etat = EtatEvenement.ModificationDemandee;

            await _evenementService.UpdateEvenementAsync(evenement);
            if (await _evenementService.SaveAsync())
                return Ok("Demande de modification enregistrée, un modérateur devra la valider.");

            return BadRequest("Erreur lors de l'enregistrement de la demande de modification.");
        }

    }
}
