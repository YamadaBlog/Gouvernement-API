using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class RessourceResource
{
    public int IdRessource { get; set; }
    public string Titre { get; set; }
    public string Contenu { get; set; }
    public string Type { get; set; }
    public int IdCreateur { get; set; }

    // Optionnel : inclure les infos sur le créateur
    public UtilisateurResource Createur { get; set; }

    public int IdEvenement { get; set; }
}
