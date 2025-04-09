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
    public class CommentaireController : ControllerBase
    {
        private readonly ICommentaireService _commentaireService;
        private readonly IMapper _mapper;

        public CommentaireController(ICommentaireService commentaireService, IMapper mapper)
        {
            _commentaireService = commentaireService;
            _mapper = mapper;
        }

        // Obtient tous les commentaires d'un événement spécifié
        [HttpGet("evenement/{evenementId}")]
        public async Task<IActionResult> GetCommentairesByEvenement(int evenementId)
        {
            var commentaires = await _commentaireService.GetCommentairesByEvenementIdAsync(evenementId);
            var commentairesResource = _mapper.Map<IEnumerable<Commentaire>, IEnumerable<CommentaireResource>>(commentaires);
            return Ok(commentairesResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentaire(int id)
        {
            var commentaire = await _commentaireService.GetCommentaireByIdAsync(id);
            if (commentaire == null)
                return NotFound();
            var commentaireResource = _mapper.Map<Commentaire, CommentaireResource>(commentaire);
            return Ok(commentaireResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentaire([FromBody] CommentaireResource commentaireResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentaire = _mapper.Map<CommentaireResource, Commentaire>(commentaireResource);
            await _commentaireService.CreateCommentaireAsync(commentaire);
            if (await _commentaireService.SaveAsync())
            {
                var result = _mapper.Map<Commentaire, CommentaireResource>(commentaire);
                return CreatedAtAction(nameof(GetCommentaire), new { id = commentaire.IdCommentaire }, result);
            }
            return BadRequest("Erreur lors de la création du commentaire");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommentaire(int id, [FromBody] CommentaireResource commentaireResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentaireFromDb = await _commentaireService.GetCommentaireByIdAsync(id);
            if (commentaireFromDb == null)
                return NotFound();

            _mapper.Map(commentaireResource, commentaireFromDb);
            await _commentaireService.UpdateCommentaireAsync(commentaireFromDb);
            if (await _commentaireService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la mise à jour du commentaire");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentaire(int id)
        {
            var commentaire = await _commentaireService.GetCommentaireByIdAsync(id);
            if (commentaire == null)
                return NotFound();
            await _commentaireService.DeleteCommentaireAsync(id);
            if (await _commentaireService.SaveAsync())
                return NoContent();
            return BadRequest("Erreur lors de la suppression du commentaire");
        }
    }
}
