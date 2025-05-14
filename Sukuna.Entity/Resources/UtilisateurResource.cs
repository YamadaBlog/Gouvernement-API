using Sukuna.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Sukuna.Common.Resources;

public class UtilisateurResource // Les ressources sont les saisies utilisateurs
{
    public int IdUtilisateur { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    [Required(ErrorMessage = "Email requis")]
    public string Email { get; set; }
    public string Role { get; set; }
    public string MotDePasse { get; set; }
    public DateTime DateCreation { get; set; }
}
