using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModerateurController : ControllerBase
    {
        private readonly IModerateurService _moderateurService;
        private readonly IMapper _mapper;

        public ModerateurController(IModerateurService moderateurService, IMapper mapper)
        {
            _moderateurService = moderateurService;
            _mapper = mapper;
        }

        // GET: api/Moderateur/evenement/{evenementId}
        [HttpGet("evenement/{evenementId}")]
        public async Task<IActionResult> GetModerateursByEvenement(int evenementId)
        {
            var moderateurs = await _moderateurService.GetModerateursByEvenementIdAsync(evenementId);
            var moderateursResource = _mapper.Map<IEnumerable<Moderateur>, IEnumerable<ModerateurResource>>(moderateurs);
            return Ok(moderateursResource);
        }

        // GET: api/Moderateur/utilisateur/{utilisateurId}
        [HttpGet("utilisateur/{utilisateurId}")]
        public async Task<IActionResult> GetModerateurByUtilisateurId(int utilisateurId)
        {
            var moderateur = await _moderateurService.GetModerateurByUtilisateurIdAsync(utilisateurId);
            if (moderateur == null)
                return NotFound();
            var moderateurResource = _mapper.Map<Moderateur, ModerateurResource>(moderateur);
            return Ok(moderateurResource);
        }

        // POST: api/Moderateur
        [HttpPost]
        public async Task<IActionResult> CreateModerateur([FromBody] ModerateurResource moderateurResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moderateur = _mapper.Map<ModerateurResource, Moderateur>(moderateurResource);
            await _moderateurService.CreateModerateurAsync(moderateur);
            if (await _moderateurService.SaveAsync())
            {
                var result = _mapper.Map<Moderateur, ModerateurResource>(moderateur);
                return CreatedAtAction(nameof(GetModerateurByUtilisateurId),
                    new { utilisateurId = moderateur.IdUtilisateur }, result);
            }
            return BadRequest("Erreur lors de la création du modérateur");
        }

        // PUT: api/Moderateur/{idUtilisateur}/{idEvenement}
        [HttpPut("{idUtilisateur}/{idEvenement}")]
        public async Task<IActionResult> UpdateModerateur(int idUtilisateur, int idEvenement, [FromBody] ModerateurResource moderateurResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moderateurFromDb = await _moderateurService.GetModerateurByUtilisateurIdAsync(idUtilisateur);
            if (moderateurFromDb == null || moderateurFromDb.IdEvenement != idEvenement)
                return NotFound();

            _mapper.Map(moderateurResource, moderateurFromDb);

            await _moderateurService.UpdateModerateurAsync(moderateurFromDb);
            if (await _moderateurService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la mise à jour du modérateur");
        }

        // DELETE: api/Moderateur/{idUtilisateur}/{idEvenement}
        [HttpDelete("{idUtilisateur}/{idEvenement}")]
        public async Task<IActionResult> DeleteModerateur(int idUtilisateur, int idEvenement)
        {
            await _moderateurService.DeleteModerateurAsync(idUtilisateur, idEvenement);
            if (await _moderateurService.SaveAsync())
                return NoContent();
            return BadRequest("Erreur lors de la suppression du modérateur");
        }
    }
}
