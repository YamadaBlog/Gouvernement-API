using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IModerateurService
{
    // Récupère les modérateurs associés à un événement donné
    Task<IEnumerable<Moderateur>> GetModerateursByEvenementIdAsync(int evenementId);

    // Récupère un modérateur par l'identifiant de l'utilisateur
    Task<Moderateur> GetModerateurByUtilisateurIdAsync(int utilisateurId);

    // Crée un nouveau modérateur
    Task CreateModerateurAsync(Moderateur moderateur);

    // Met à jour un modérateur
    Task UpdateModerateurAsync(Moderateur moderateur);

    // Supprime un modérateur en fonction de l'id utilisateur et de l’événement
    Task DeleteModerateurAsync(int utilisateurId, int evenementId);

    // Sauvegarde des modifications dans la BDD
    Task<bool> SaveAsync();
}
