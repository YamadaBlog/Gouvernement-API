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

        // Récupère le modérateur associé à un événement spécifique (si applicable)
        public async Task<Moderateur> GetModerateurByEvenementIdAsync(int evenementId)
        {
            return await _context.Evenements
                                 .Where(e => e.IdEvenement == evenementId)
                                 .Select(e => e.Moderateur)
                                 .FirstOrDefaultAsync();
        }


        // Récupère un modérateur par son identifiant propre
        public async Task<Moderateur> GetModerateurByIdAsync(int id)
        {
            return await _context.Moderateurs.FirstOrDefaultAsync(m => m.IdModerateur == id);
        }

        public async Task CreateModerateurAsync(Moderateur moderateur)
        {
            await _context.Moderateurs.AddAsync(moderateur);
        }

        public async Task UpdateModerateurAsync(Moderateur moderateur)
        {
            _context.Moderateurs.Update(moderateur);
        }

        // Supprime un modérateur par son identifiant propre
        public async Task DeleteModerateurAsync(int id)
        {
            var moderateur = await _context.Moderateurs.FirstOrDefaultAsync(m => m.IdModerateur == id);
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
