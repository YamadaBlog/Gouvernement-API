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
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly IMapper _mapper;

        public UtilisateurController(IUtilisateurService utilisateurService, IMapper mapper)
        {
            _utilisateurService = utilisateurService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUtilisateurs()
        {
            var utilisateurs = await _utilisateurService.GetAllUtilisateursAsync();
            var utilisateursResource = _mapper.Map<IEnumerable<Utilisateur>, IEnumerable<UtilisateurResource>>(utilisateurs);
            return Ok(utilisateursResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilisateur(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
                return NotFound();
            var utilisateurResource = _mapper.Map<Utilisateur, UtilisateurResource>(utilisateur);
            return Ok(utilisateurResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUtilisateur([FromBody] UtilisateurResource utilisateurResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var utilisateur = _mapper.Map<UtilisateurResource, Utilisateur>(utilisateurResource);
            await _utilisateurService.CreateUtilisateurAsync(utilisateur);
            if (await _utilisateurService.SaveAsync())
            {
                var result = _mapper.Map<Utilisateur, UtilisateurResource>(utilisateur);
                return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.IdUtilisateur }, result);
            }
            return BadRequest("Erreur lors de la création de l'utilisateur");
        }

        [HttpPost("{userEmail},{userMpd}")]
        public async Task<IActionResult> GetAuthauthUser(string userEmail, string userMpd)
        {
            var utilisateur = await _utilisateurService.GetAuthauthUser(userEmail, userMpd);

            if (utilisateur == null)
                return Unauthorized("L'authentification a échoué : utilisateur non trouvé ou mot de passe incorrect.");

            var userResource = _mapper.Map<UtilisateurResource>(utilisateur);

            return Ok(userResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUtilisateur(int id, [FromBody] UtilisateurResource utilisateurResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var utilisateurFromDb = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateurFromDb == null)
                return NotFound();

            _mapper.Map(utilisateurResource, utilisateurFromDb);

            await _utilisateurService.UpdateUtilisateurAsync(utilisateurFromDb);
            if (await _utilisateurService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la mise à jour de l'utilisateur");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            if (utilisateur == null)
                return NotFound();
            await _utilisateurService.DeleteUtilisateurAsync(id);
            if (await _utilisateurService.SaveAsync())
                return NoContent();
            return BadRequest("Erreur lors de la suppression de l'utilisateur");
        }
    }
}
