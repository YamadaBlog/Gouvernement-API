using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models
{
    public class Badge
    {
        [Key]
        public int IdBadge { get; set; }

        public string Description { get; set; }

        public int Points { get; set; }

        // Relation : un Badge peut être associé à plusieurs événements
        public ICollection<Evenement> Evenements { get; set; }
    }

}
