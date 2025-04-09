using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class Statistique
    {
        [Key]
        public int IdStat { get; set; }

        public int IdUtilisateur { get; set; }
        [ForeignKey("IdUtilisateur")]
        public Utilisateur Utilisateur { get; set; }

        public int IdEvenement { get; set; }
        [ForeignKey("IdEvenement")]
        public Evenement Evenement { get; set; }

        public string Action { get; set; }

        public DateTime Date { get; set; }
    }

}
