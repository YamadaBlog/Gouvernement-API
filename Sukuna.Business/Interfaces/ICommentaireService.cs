using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface ICommentaireService
{
    // Récupère tous les commentaires associés à un événement spécifique
    Task<IEnumerable<Commentaire>> GetCommentairesByEvenementIdAsync(int evenementId);

    // Récupère un commentaire par son identifiant
    Task<Commentaire> GetCommentaireByIdAsync(int id);

    // Ajoute un nouveau commentaire
    Task CreateCommentaireAsync(Commentaire commentaire);

    // Met à jour un commentaire existant
    Task UpdateCommentaireAsync(Commentaire commentaire);

    // Supprime un commentaire par son identifiant
    Task DeleteCommentaireAsync(int id);

    // Sauvegarde des modifications dans la BDD
    Task<bool> SaveAsync();
}
