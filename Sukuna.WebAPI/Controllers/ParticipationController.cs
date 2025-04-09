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
    public class ParticipationController : ControllerBase
    {
        private readonly IParticipationService _participationService;
        private readonly IMapper _mapper;

        public ParticipationController(IParticipationService participationService, IMapper mapper)
        {
            _participationService = participationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipations()
        {
            var participations = await _participationService.GetAllParticipationsAsync();
            var participationsResource = _mapper.Map<IEnumerable<Participation>, IEnumerable<ParticipationResource>>(participations);
            return Ok(participationsResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipation(int id)
        {
            var participation = await _participationService.GetParticipationByIdAsync(id);
            if (participation == null)
                return NotFound();
            var participationResource = _mapper.Map<Participation, ParticipationResource>(participation);
            return Ok(participationResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipation([FromBody] ParticipationResource participationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var participation = _mapper.Map<ParticipationResource, Participation>(participationResource);
            await _participationService.CreateParticipationAsync(participation);
            if (await _participationService.SaveAsync())
            {
                var result = _mapper.Map<Participation, ParticipationResource>(participation);
                return CreatedAtAction(nameof(GetParticipation), new { id = participation.IdParticipation }, result);
            }
            return BadRequest("Erreur lors de la création de la participation");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipation(int id, [FromBody] ParticipationResource participationResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var participationFromDb = await _participationService.GetParticipationByIdAsync(id);
            if (participationFromDb == null)
                return NotFound();

            _mapper.Map(participationResource, participationFromDb);
            await _participationService.UpdateParticipationAsync(participationFromDb);
            if (await _participationService.SaveAsync())
                return NoContent();

            return BadRequest("Erreur lors de la mise à jour de la participation");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipation(int id)
        {
            var participation = await _participationService.GetParticipationByIdAsync(id);
            if (participation == null)
                return NotFound();
            await _participationService.DeleteParticipationAsync(id);
            if (await _participationService.SaveAsync())
                return NoContent();
            return BadRequest("Erreur lors de la suppression de la participation");
        }
    }
}
