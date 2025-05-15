using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukuna.Common.Models
{
    public enum EtatEvenement
    {
        EnAttente,
        Valide,
        ModificationDemandee
    }

    public class Evenement
    {
        [Key]
        public int IdEvenement { get; set; }

        [Required]
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Lieu { get; set; }
        public string Type { get; set; }
        public int NombreParticipantsMin { get; set; }
        public int NombreParticipantsMax { get; set; }
        public string Accessibilite { get; set; }
        public DateTime DateCreation { get; set; }

        // Organisateur
        public int IdOrganisateur { get; set; }
        [ForeignKey("IdOrganisateur")]
        public Utilisateur Organisateur { get; set; }

        // Badge éventuel
        public int? IdBadge { get; set; }
        [ForeignKey("IdBadge")]
        public Badge Badge { get; set; }

        // Nouveaux champs métier
        public EtatEvenement Etat { get; set; } = EtatEvenement.EnAttente;
        public DateTime? DateValidation { get; set; }

        // On stocke ici l’ID de l’utilisateur (modérateur) qui a validé
        public int? IdModerateur { get; set; }
        [ForeignKey("IdModerateur")]
        public Utilisateur Moderateur { get; set; }

        // Relations
        public ICollection<Participation> Participations { get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Ressource> Ressources { get; set; }
        public ICollection<Statistique> Statistiques { get; set; }
    }
}
