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
        public async Task<IActionResult> GetModerateurByEvenement(int evenementId)
        {
            var moderateur = await _moderateurService.GetModerateurByEvenementIdAsync(evenementId);
            if (moderateur == null)
                return NotFound();
            var moderateurResource = _mapper.Map<Moderateur, ModerateurResource>(moderateur);
            return Ok(moderateurResource);
        }

        // GET: api/Moderateur/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModerateurById(int id)
        {
            var moderateur = await _moderateurService.GetModerateurByIdAsync(id);
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
                return CreatedAtAction(nameof(GetModerateurById),
                    new { id = moderateur.IdModerateur }, result);
            }
            return BadRequest("Erreur lors de la création du modérateur");
        }

        // PUT: api/Moderateur/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModerateur(int id, [FromBody] ModerateurResource moderateurResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var moderateurFromDb = await _moderateurService.GetModerateurByIdAsync(id);
            if (moderateurFromDb == null)
                return NotFound();

            _mapper.Map(moderateurResource, moderateurFromDb);

            await _moderateurService.UpdateModerateurAsync(moderateurFromDb);
            if (await _moderateurService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la mise à jour du modérateur");
        }

        // DELETE: api/Moderateur/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModerateur(int id)
        {
            await _moderateurService.DeleteModerateurAsync(id);
            if (await _moderateurService.SaveAsync())
                return NoContent();
            return BadRequest("Erreur lors de la suppression du modérateur");
        }
    }
}
