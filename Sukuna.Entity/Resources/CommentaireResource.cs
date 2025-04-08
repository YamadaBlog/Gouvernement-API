using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class CommentaireResource
{
    public int IdCommentaire { get; set; }
    public int IdUtilisateur { get; set; }
    public int IdEvenement { get; set; }
    public string Contenu { get; set; }
    public DateTime Date { get; set; }

    // Optionnel : inclure les infos sur l'utilisateur auteur
    public UtilisateurResource Utilisateur { get; set; }
}
