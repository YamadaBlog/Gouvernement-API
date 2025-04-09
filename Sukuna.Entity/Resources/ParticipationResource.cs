using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class ParticipationResource
{
    public int IdParticipation { get; set; }
    public int IdUtilisateur { get; set; }
    public int IdEvenement { get; set; }
    public string RoleParticipation { get; set; }
    public DateTime DateParticipation { get; set; }

    // Optionnel : inclure les infos sur l'utilisateur
    public UtilisateurResource Utilisateur { get; set; }
}
