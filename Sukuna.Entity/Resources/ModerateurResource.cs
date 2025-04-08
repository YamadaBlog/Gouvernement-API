using Sukuna.Common.Models;

namespace Sukuna.Common.Resources;

public class ModerateurResource
{
    public int IdUtilisateur { get; set; }
    public int IdEvenement { get; set; }
    public string StatutValidation { get; set; }
    public DateTime DateValidation { get; set; }
}
