using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukuna.Common.Models;

public class Interaction

{
    [Key]
    public int Id { get; set; }

    public int IdUtilisateur { get; set; }
    [ForeignKey("IdUtilisateur")]
    public Utilisateur Utilisateur { get; set; }

    public int IdBadge { get; set; }
    [ForeignKey("IdBadge")]
    public Badge Badge { get; set; }

    public DateTime Date { get; set; }
}

