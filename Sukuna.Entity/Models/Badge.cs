using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Models
{
    public class Badge
    {
        [Key]
        public int IdBadge { get; set; }

        public string Description { get; set; }
        public int Points { get; set; }

        // Pour la relation avec Interaction
        public ICollection<Interaction> Interactions { get; set; }

        // Si la relation entre Badge et Evenement existe
        public ICollection<Evenement> Evenements { get; set; }
    }
}
