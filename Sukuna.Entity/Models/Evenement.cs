using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukuna.Common.Models
{
    public enum EtatEvenement
    {
        EnAttente,              // Créé par l'utilisateur, en attente de validation par un modérateur
        Valide,                 // Validé par un modérateur et publié ; l'utilisateur ne peut plus le modifier directement
        ModificationDemandee    // L'utilisateur a demandé une modification après validation, et cette demande est en cours de traitement par un modérateur
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

        // Clé étrangère pour l'organisateur
        public int IdOrganisateur { get; set; }
        [ForeignKey("IdOrganisateur")]
        public Utilisateur Organisateur { get; set; }

        // Association optionnelle à Badge
        public int? IdBadge { get; set; }
        [ForeignKey("IdBadge")]
        public Badge Badge { get; set; }

        // Nouvel attribut pour l'état de l'événement
        public EtatEvenement Etat { get; set; } = EtatEvenement.EnAttente;

        // Date à laquelle l'événement a été validé par un modérateur (optionnel)
        public DateTime? DateValidation { get; set; }

        // Relation optionnelle pour la validation par un modérateur
        public int? IdModerateur { get; set; }
        [ForeignKey("IdModerateur")]
        public Moderateur Moderateur { get; set; }

        // Propriétés de navigation pour les autres entités
        public ICollection<Participation> Participations { get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Ressource> Ressources { get; set; }
        public ICollection<Statistique> Statistiques { get; set; }
    }
}
