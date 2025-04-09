using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;

namespace Sukuna.Service
{
    public class ParticipationService : IParticipationService
    {
        private readonly DataContext _context;

        public ParticipationService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Participation>> GetAllParticipationsAsync()
        {
            return await _context.Participations.ToListAsync();
        }

        public async Task<Participation> GetParticipationByIdAsync(int id)
        {
            return await _context.Participations.FindAsync(id);
        }

        public async Task CreateParticipationAsync(Participation participation)
        {
            await _context.Participations.AddAsync(participation);
        }

        public async Task UpdateParticipationAsync(Participation participation)
        {
            _context.Participations.Update(participation);
        }

        public async Task DeleteParticipationAsync(int id)
        {
            var participation = await GetParticipationByIdAsync(id);
            if (participation != null)
            {
                _context.Participations.Remove(participation);
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
