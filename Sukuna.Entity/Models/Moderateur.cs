using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukuna.Common.Models
{
    public class Moderateur
    {
        [Key]
        public int IdModerateur { get; set; }  // Identifiant propre du modérateur, auto-incrémenté

        // Propriété de navigation pour récupérer tous les événements validés par ce modérateur
        public ICollection<Evenement> EvenementsValides { get; set; } = new List<Evenement>();

        public string StatutValidation { get; set; }
        public DateTime DateValidation { get; set; }
    }
}
