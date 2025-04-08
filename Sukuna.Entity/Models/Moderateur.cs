using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class Moderateur
    {
        [Key]
        public int IdUtilisateur { get; set; }

        public int IdEvenement { get; set; }
        [ForeignKey("IdEvenement")]
        public Evenement Evenement { get; set; }

        public string StatutValidation { get; set; }

        public DateTime DateValidation { get; set; }
    }
}
