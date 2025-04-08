using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IUtilisateurService
{
    // Récupère la liste de tous les utilisateurs
    Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync();

    // Récupère un utilisateur par son identifiant
    Task<Utilisateur> GetUtilisateurByIdAsync(int id);

    // Crée un nouvel utilisateur
    Task CreateUtilisateurAsync(Utilisateur utilisateur);

    // Met à jour un utilisateur existant
    Task UpdateUtilisateurAsync(Utilisateur utilisateur);

    // Supprime un utilisateur par son identifiant
    Task DeleteUtilisateurAsync(int id);

    // Sauvegarde des modifications dans la BDD
    Task<bool> SaveAsync();
}