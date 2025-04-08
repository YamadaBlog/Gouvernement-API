using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukuna.Common.Models
{

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

        // FK optionnelles
        public int? IdBadge { get; set; }
        [ForeignKey("IdBadge")]
        public Badge Badge { get; set; }

        public int? IdModerateur { get; set; }
        // On peut choisir d’omettre cette navigation si la relation Moderateur est gérée via une table d’association
        [ForeignKey("IdModerateur")]
        public Moderateur Moderateur { get; set; }

        // L'organisateur est obligatoire
        public int IdOrganisateur { get; set; }
        [ForeignKey("IdOrganisateur")]
        public Utilisateur Organisateur { get; set; }

        // Navigation properties
        public ICollection<Participation> Participations { get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Ressource> Ressources { get; set; }
        public ICollection<Statistique> Statistiques { get; set; }
    }
}