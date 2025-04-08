using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MotDePasse { get; set; }

        public string Role { get; set; }

        public DateTime DateCreation { get; set; }

        // Navigation properties
        public ICollection<Participation> Participations { get; set; }
        public ICollection<Commentaire> Commentaires { get; set; }

        // En tant qu’organisateur d’événements
        public ICollection<Evenement> EvenementsOrganises { get; set; }

        // Si l’utilisateur peut créer des ressources
        public ICollection<Ressource> RessourcesCreees { get; set; }
    }
}