using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;

namespace Sukuna.Service
{
    public class ModerateurService : IModerateurService
    {
        private readonly DataContext _context;

        public ModerateurService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Moderateur>> GetModerateursByEvenementIdAsync(int evenementId)
        {
            return await _context.Moderateurs
                                 .Where(m => m.IdEvenement == evenementId)
                                 .ToListAsync();
        }

        public async Task<Moderateur> GetModerateurByUtilisateurIdAsync(int utilisateurId)
        {
            return await _context.Moderateurs.FirstOrDefaultAsync(m => m.IdUtilisateur == utilisateurId);
        }

        public async Task CreateModerateurAsync(Moderateur moderateur)
        {
            await _context.Moderateurs.AddAsync(moderateur);
        }

        public async Task UpdateModerateurAsync(Moderateur moderateur)
        {
            _context.Moderateurs.Update(moderateur);
        }

        public async Task DeleteModerateurAsync(int utilisateurId, int evenementId)
        {
            var moderateur = await _context.Moderateurs.FirstOrDefaultAsync(m => m.IdUtilisateur == utilisateurId && m.IdEvenement == evenementId);
            if (moderateur != null)
            {
                _context.Moderateurs.Remove(moderateur);
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
