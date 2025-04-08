using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class Commentaire
    {
        [Key]
        public int IdCommentaire { get; set; }

        public int IdUtilisateur { get; set; }
        [ForeignKey("IdUtilisateur")]
        public Utilisateur Utilisateur { get; set; }

        public int IdEvenement { get; set; }
        [ForeignKey("IdEvenement")]
        public Evenement Evenement { get; set; }

        public string Contenu { get; set; }

        public DateTime Date { get; set; }
    }
}
