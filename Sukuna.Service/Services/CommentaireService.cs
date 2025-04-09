using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.DataAccess.Data;

namespace Sukuna.Service
{
    public class CommentaireService : ICommentaireService
    {
        private readonly DataContext _context;

        public CommentaireService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Commentaire>> GetCommentairesByEvenementIdAsync(int evenementId)
        {
            return await _context.Commentaires
                                 .Where(c => c.IdEvenement == evenementId)
                                 .ToListAsync();
        }

        public async Task<Commentaire> GetCommentaireByIdAsync(int id)
        {
            return await _context.Commentaires.FindAsync(id);
        }

        public async Task CreateCommentaireAsync(Commentaire commentaire)
        {
            await _context.Commentaires.AddAsync(commentaire);
        }

        public async Task UpdateCommentaireAsync(Commentaire commentaire)
        {
            _context.Commentaires.Update(commentaire);
        }

        public async Task DeleteCommentaireAsync(int id)
        {
            var commentaire = await GetCommentaireByIdAsync(id);
            if (commentaire != null)
            {
                _context.Commentaires.Remove(commentaire);
            }
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
