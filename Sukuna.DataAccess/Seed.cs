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
            // 1) Si la BDD contient déjà des données, on purge tout
            if (context.Utilisateurs.Any() || context.Evenements.Any())
            {
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

            // 2) Création des utilisateurs
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
            var modUser = new Utilisateur
            {
                Nom = "Admin",
                Prenom = "Alice",
                Email = "alice.admin@example.com",
                MotDePasse = "adminpass",
                Role = "Moderateur",
                DateCreation = DateTime.UtcNow
            };

            context.Utilisateurs.AddRange(user1, user2, modUser);
            context.SaveChanges();

            // 3) Création d'un Badge
            var badge1 = new Badge
            {
                Description = "Badge Débutant",
                Points = 10
            };
            context.Badges.Add(badge1);
            context.SaveChanges();

            // 4) Création d'événements
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
                IdOrganisateur = user1.IdUtilisateur,
                IdBadge = badge1.IdBadge,
                Etat = EtatEvenement.EnAttente
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
                IdOrganisateur = user2.IdUtilisateur,
                IdBadge = badge1.IdBadge,
                Etat = EtatEvenement.EnAttente
            };
            // événement déjà validé par le modérateur
            var event3 = new Evenement
            {
                Titre = "Conférence React",
                Description = "Introduction à React et son écosystème",
                Date = DateTime.UtcNow.AddDays(20),
                Lieu = "Marseille",
                Type = "Webinaire",
                NombreParticipantsMin = 10,
                NombreParticipantsMax = 100,
                Accessibilite = "Public",
                DateCreation = DateTime.UtcNow,
                IdOrganisateur = user1.IdUtilisateur,
                IdBadge = badge1.IdBadge,
                Etat = EtatEvenement.Valide,
                DateValidation = DateTime.UtcNow,
                IdModerateur = modUser.IdUtilisateur
            };

            context.Evenements.AddRange(event1, event2, event3);
            context.SaveChanges();

            // 5) Commentaires
            var commentaire1 = new Commentaire
            {
                IdUtilisateur = user2.IdUtilisateur,
                IdEvenement = event1.IdEvenement,
                Contenu = "J'attends avec impatience ce séminaire !",
                Date = DateTime.UtcNow
            };
            var commentaire2 = new Commentaire
            {
                IdUtilisateur = user1.IdUtilisateur,
                IdEvenement = event3.IdEvenement,
                Contenu = "Super événement, hâte d'y être.",
                Date = DateTime.UtcNow
            };
            context.Commentaires.AddRange(commentaire1, commentaire2);
            context.SaveChanges();

            // 6) Participations
            var participation1 = new Participation
            {
                IdUtilisateur = user1.IdUtilisateur,
                IdEvenement = event1.IdEvenement,
                RoleParticipation = "Organisateur",
                DateParticipation = DateTime.UtcNow
            };
            var participation2 = new Participation
            {
                IdUtilisateur = user2.IdUtilisateur,
                IdEvenement = event3.IdEvenement,
                RoleParticipation = "Participant",
                DateParticipation = DateTime.UtcNow
            };
            context.Participations.AddRange(participation1, participation2);
            context.SaveChanges();
        }
    }
}
