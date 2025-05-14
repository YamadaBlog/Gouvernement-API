using Sukuna.Common.Models;
using System;
using System.Collections.Generic;

namespace Sukuna.Common.Resources
{
    public class EvenementResource
    {
        public int IdEvenement { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Lieu { get; set; }
        public string Type { get; set; }
        public int NombreParticipantsMin { get; set; }
        public int NombreParticipantsMax { get; set; }
        public string Accessibilite { get; set; }
        public DateTime DateCreation { get; set; }

        // Nouveaux champs métier
        public EtatEvenement Etat { get; set; }
        public DateTime? DateValidation { get; set; }

        // On expose l’ID du validateur et son resource
        public int? IdModerateur { get; set; }
        public UtilisateurResource Moderateur { get; set; }

        // Organisateur
        public int IdOrganisateur { get; set; }
        public UtilisateurResource Organisateur { get; set; }

        // Badge éventuel
        public BadgeResource Badge { get; set; }

        // Collections
        public IEnumerable<ParticipationResource> Participations { get; set; }
        public IEnumerable<CommentaireResource> Commentaires { get; set; }
        public IEnumerable<RessourceResource> Ressources { get; set; }
    }
}
