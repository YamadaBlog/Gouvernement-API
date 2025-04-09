using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class InteractionResource
{
    public int Id { get; set; }
    public int IdUtilisateur { get; set; }
    public int IdBadge { get; set; }
    public DateTime Date { get; set; }

    public UtilisateurResource Utilisateur { get; set; }
    public BadgeResource Badge { get; set; }
}
