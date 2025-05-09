﻿using System.Collections.Generic;
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
            return await _context.Evenements.ToListAsync();
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

        public async Task<bool> UpdateEvenementAsync(Evenement evenement)
        {
            if (evenement.Etat == EtatEvenement.Valide)
            {
                throw new InvalidOperationException("L'événement est validé. Pour toute modification, veuillez émettre une demande de modification.");
            }

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
