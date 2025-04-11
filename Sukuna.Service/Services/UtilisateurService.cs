using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;

namespace Sukuna.Service
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly DataContext _context;

        public UtilisateurService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
        {
            return await _context.Utilisateurs.ToListAsync();
        }

        public async Task<Utilisateur> GetUtilisateurByIdAsync(int id)
        {
            return await _context.Utilisateurs.FindAsync(id);
        }

        public async Task CreateUtilisateurAsync(Utilisateur utilisateur)
        {
            await _context.Utilisateurs.AddAsync(utilisateur);
        }

        public async Task<Utilisateur> GetAuthauthUser(string userEmail, string userMpd)
        {
            return await _context.Utilisateurs
                .FirstOrDefaultAsync(c => c.Email == userEmail && c.MotDePasse == userMpd);
        }

        public async Task UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Update(utilisateur);
        }

        public async Task DeleteUtilisateurAsync(int id)
        {
            var utilisateur = await GetUtilisateurByIdAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
