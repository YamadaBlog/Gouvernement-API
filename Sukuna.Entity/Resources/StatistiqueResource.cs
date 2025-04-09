using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class StatistiqueResource
{
    public int IdStat { get; set; }
    public int IdUtilisateur { get; set; }
    public int IdEvenement { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }

    public UtilisateurResource Utilisateur { get; set; }
}
