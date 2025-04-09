using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Models;

public class Ressource

{
    [Key]
    public int IdRessource { get; set; }

    public string Titre { get; set; }

    public string Contenu { get; set; }

    public string Type { get; set; }

    public int IdCreateur { get; set; }
    [ForeignKey("IdCreateur")]
    public Utilisateur Createur { get; set; }

    public int IdEvenement { get; set; }
    [ForeignKey("IdEvenement")]
    public Evenement Evenement { get; set; }
}
