using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;

namespace Sukuna.Service
{
    public class EvenementService : IEvenementService
    {
        private readonly DataContext _context;

        public EvenementService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evenement>> GetAllEvenementsAsync()
        {
            return await _context.Evenements
                     .Where(e => e.Etat == EtatEvenement.EnAttente)
                     .ToListAsync();
        }

        public async Task<IEnumerable<Evenement>> GetValidatedEvenementsAsync()
        {
            return await _context.Evenements
                     .Where(e => e.Etat == EtatEvenement.Valide)
                     .ToListAsync();
        }

        public async Task<Evenement> GetEvenementByIdAsync(int id)
        {
            return await _context.Evenements.FindAsync(id);
        }

        public async Task CreateEvenementAsync(Evenement evenement)
        {
            // Forcer l'état initial à EnAttente, même si le client a envoyé une autre valeur
            evenement.Etat = EtatEvenement.EnAttente;
            await _context.Evenements.AddAsync(evenement);
        }

        public async Task ValidateEvenementAsync(int idEvenement, int idModerateur)
        {
            var evt = await _context.Evenements
                                    .FirstOrDefaultAsync(e => e.IdEvenement == idEvenement);

            if (evt == null)
                throw new KeyNotFoundException($"Événement {idEvenement} introuvable.");

            if (evt.Etat != EtatEvenement.EnAttente)
                throw new InvalidOperationException("Seuls les événements en attente peuvent être validés.");

            evt.Etat = EtatEvenement.Valide;
            evt.DateValidation = DateTime.UtcNow;
            evt.IdModerateur = idModerateur;

            // EF Core saura détecter la modification
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateEvenementAsync(Evenement evenement)
        {
            _context.Evenements.Update(evenement);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task DeleteEvenementAsync(int id)
        {
            var evenement = await GetEvenementByIdAsync(id);
            if (evenement != null)
            {
                _context.Evenements.Remove(evenement);
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
