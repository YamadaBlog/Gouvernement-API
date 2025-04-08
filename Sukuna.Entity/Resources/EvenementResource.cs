using Sukuna.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Common.Resources;

public class EvenementResource
{
    public int IdEvenement { get; set; }
    public string Titre { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Lieu { get; set; }
    public string Type { get; set; }
    public int NombreParticipantsMin { get; set; }
    public int NombreParticipantsMax { get; set; }
    public string Accessibilite { get; set; }
    public DateTime DateCreation { get; set; }

    // Relations facultatives pouvant être imbriquées
    public UtilisateurResource Organisateur { get; set; }
    public BadgeResource Badge { get; set; }

    // Listes pour afficher des informations supplémentaires si besoin
    public IEnumerable<ParticipationResource> Participations { get; set; }
    public IEnumerable<CommentaireResource> Commentaires { get; set; }
    public IEnumerable<RessourceResource> Ressources { get; set; }
}
