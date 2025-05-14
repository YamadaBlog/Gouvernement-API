using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IEvenementService
{
    // Récupère la liste de tous les événements
    Task<IEnumerable<Evenement>> GetAllEvenementsAsync();

    // Nouvelle méthode pour récupérer uniquement les événements validés
    Task<IEnumerable<Evenement>> GetValidatedEvenementsAsync();

    // Récupère un événement par son identifiant
    Task<Evenement> GetEvenementByIdAsync(int id);

    // Création d'un nouvel événement
    Task CreateEvenementAsync(Evenement evenement);

    // Mise à jour d'un événement existant
    Task<bool> UpdateEvenementAsync(Evenement evenement);

    Task ValidateEvenementAsync(int idEvenement, int idModerateur);

    // Suppression d'un événement par son identifiant
    Task DeleteEvenementAsync(int id);

    // Sauvegarde des modifications dans la BDD
    Task<bool> SaveAsync();
}
