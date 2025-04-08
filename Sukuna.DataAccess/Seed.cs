using System;
using System.Linq;
using Sukuna.DataAccess.Data;
using Sukuna.Common.Models;

namespace Sukuna.DataAccess
{
    public class Seed
    {
        private readonly DataContext context;

        public Seed(DataContext context)
        {
            this.context = context;
        }

        public void SeedDataContext()
        {
            // Vérifier si la base contient déjà des données dans les entités principales
            if (context.Utilisateurs.Any() || context.Evenements.Any())
            {
                // Supprimer les données dans un ordre respectant les clés étrangères
                context.Moderateurs.RemoveRange(context.Moderateurs);
                context.Commentaires.RemoveRange(context.Commentaires);
                context.Participations.RemoveRange(context.Participations);
                context.Ressources.RemoveRange(context.Ressources);
                context.Badges.RemoveRange(context.Badges);
                context.Interactions.RemoveRange(context.Interactions);
                context.Statistiques.RemoveRange(context.Statistiques);
                context.Evenements.RemoveRange(context.Evenements);
                context.Utilisateurs.RemoveRange(context.Utilisateurs);

                context.SaveChanges();
            }

            // Création d'utilisateurs
            var user1 = new Utilisateur
            {
                Nom = "Dupont",
                Prenom = "Jean",
                Email = "jean.dupont@example.com",
                MotDePasse = "password",
                Role = "User",
                DateCreation = DateTime.UtcNow
            };

            var user2 = new Utilisateur
            {
                Nom = "Durand",
                Prenom = "Marie",
                Email = "marie.durand@example.com",
                MotDePasse = "password",
                Role = "User",
                DateCreation = DateTime.UtcNow
            };

            // Utilisateur qui jouera le rôle de modérateur
            var modUser = new Utilisateur
            {
                Nom = "Admin",
                Prenom = "Alice",
                Email = "admin@example.com",
                MotDePasse = "adminpass",
                Role = "Moderateur",
                DateCreation = DateTime.UtcNow
            };

            context.Utilisateurs.AddRange(user1, user2, modUser);
            context.SaveChanges();

            // Création d'un Badge (optionnel)
            var badge1 = new Badge
            {
                Description = "Badge Débutant",
                Points = 10
            };
            context.Badges.Add(badge1);
            context.SaveChanges();

            // Création d'événements (séminaires)
            var event1 = new Evenement
            {
                Titre = "Séminaire .NET",
                Description = "Séminaire sur les nouveautés de .NET",
                Date = DateTime.UtcNow.AddDays(10),
                Lieu = "Paris",
                Type = "Webinaire",
                NombreParticipantsMin = 5,
                NombreParticipantsMax = 50,
                Accessibilite = "Public",
                DateCreation = DateTime.UtcNow,
                // L'organisateur est user1
                IdOrganisateur = user1.IdUtilisateur,
                // Association du badge à l'événement (optionnel)
                IdBadge = badge1.IdBadge
            };

            var event2 = new Evenement
            {
                Titre = "Atelier Angular",
                Description = "Atelier pour apprendre Angular",
                Date = DateTime.UtcNow.AddDays(15),
                Lieu = "Lyon",
                Type = "Physique",
                NombreParticipantsMin = 3,
                NombreParticipantsMax = 30,
                Accessibilite = "Privé",
                DateCreation = DateTime.UtcNow,
                // L'organisateur est user2
                IdOrganisateur = user2.IdUtilisateur,
                IdBadge = badge1.IdBadge
            };

            context.Evenements.AddRange(event1, event2);
            context.SaveChanges();

            // Création d'un modérateur pour l'événement 1
            var moderateur = new Moderateur
            {
                // Le modérateur est le modUser
                IdUtilisateur = modUser.IdUtilisateur,
                IdEvenement = event1.IdEvenement,
                StatutValidation = "En attente",
                DateValidation = DateTime.UtcNow
            };

            context.Moderateurs.Add(moderateur);
            context.SaveChanges();

            // Création d'un commentaire sur l'événement 1 par user2
            var commentaire = new Commentaire
            {
                IdUtilisateur = user2.IdUtilisateur,
                IdEvenement = event1.IdEvenement,
                Contenu = "J'attends avec impatience ce séminaire !",
                Date = DateTime.UtcNow
            };

            context.Commentaires.Add(commentaire);
            context.SaveChanges();

            // Exemple de participation : user1 s'inscrit à l'événement 1 en tant qu'organisateur (ou autre rôle)
            var participation = new Participation
            {
                IdUtilisateur = user1.IdUtilisateur,
                IdEvenement = event1.IdEvenement,
                RoleParticipation = "Organisateur",
                DateParticipation = DateTime.UtcNow
            };

            context.Participations.Add(participation);
            context.SaveChanges();
        }
    }
}
