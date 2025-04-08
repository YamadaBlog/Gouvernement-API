using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IParticipationService
{
    // Récupère toutes les participations
    Task<IEnumerable<Participation>> GetAllParticipationsAsync();

    // Récupère une participation par son identifiant
    Task<Participation> GetParticipationByIdAsync(int id);

    // Crée une nouvelle participation
    Task CreateParticipationAsync(Participation participation);

    // Met à jour une participation existante
    Task UpdateParticipationAsync(Participation participation);

    // Supprime une participation par son identifiant
    Task DeleteParticipationAsync(int id);

    // Sauvegarde des modifications dans la BDD
    Task<bool> SaveAsync();
}
