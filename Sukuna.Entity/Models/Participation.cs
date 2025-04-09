using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
public class Participation
    {
        [Key]
        public int IdParticipation { get; set; }

        // Relations
        public int IdUtilisateur { get; set; }
        [ForeignKey("IdUtilisateur")]
        public Utilisateur Utilisateur { get; set; }

        public int IdEvenement { get; set; }
        [ForeignKey("IdEvenement")]
        public Evenement Evenement { get; set; }

        public string RoleParticipation { get; set; }

        public DateTime DateParticipation { get; set; }
    }

}
