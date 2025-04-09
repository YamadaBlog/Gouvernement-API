using Sukuna.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces
{
    public interface IModerateurService
    {
        // Récupère le modérateur associé à un événement donné (si applicable)
        Task<Moderateur> GetModerateurByEvenementIdAsync(int evenementId);

        // Récupère un modérateur par son identifiant propre
        Task<Moderateur> GetModerateurByIdAsync(int id);

        // Crée un nouveau modérateur
        Task CreateModerateurAsync(Moderateur moderateur);

        // Met à jour un modérateur
        Task UpdateModerateurAsync(Moderateur moderateur);

        // Supprime un modérateur par son identifiant propre
        Task DeleteModerateurAsync(int id);

        // Sauvegarde des modifications dans la BDD
        Task<bool> SaveAsync();
    }
}
