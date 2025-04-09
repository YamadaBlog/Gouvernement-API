using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> GetAllEvenements()
        {
            var evenements = await _evenementService.GetAllEvenementsAsync();
            var evenementsResource = _mapper.Map<IEnumerable<Evenement>, IEnumerable<EvenementResource>>(evenements);
            return Ok(evenementsResource);
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

            return BadRequest("Erreur lors de la mise à jour de l'événement");
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

        // Endpoint optionnel pour permettre au modérateur d'approuver un événement
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveEvenement(int id)
        {
            var evenement = await _evenementService.GetEvenementByIdAsync(id);
            if (evenement == null)
                return NotFound();

            // Exemple de logique d'approbation :
            if (evenement.Moderateur != null)
            {
                evenement.Moderateur.StatutValidation = "Validé";
                await _evenementService.UpdateEvenementAsync(evenement);
                if (await _evenementService.SaveAsync())
                    return Ok(new { message = "L'événement a été validé." });
            }
            return BadRequest("Erreur lors de la validation de l'événement");
        }
    }
}
