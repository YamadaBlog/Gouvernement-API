using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Email requis")]
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Role { get; set; }
        public DateTime DateCreation { get; set; }

        // Propriétés de navigation existantes
        public ICollection<Evenement> EvenementsValides { get; set; }
        public ICollection<Participation> Participations { get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Evenement> EvenementsOrganises { get; set; }
        public ICollection<Ressource> RessourcesCreees { get; set; }

        // Ajoute la collection pour les interactions
        public ICollection<Interaction> Interactions { get; set; }

        // Optionnel : si tu utilises des statistiques
        public ICollection<Statistique> Statistiques { get; set; }
    }
}
