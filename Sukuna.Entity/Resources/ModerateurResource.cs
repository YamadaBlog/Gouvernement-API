using System;

namespace Sukuna.Common.Resources
{
    public class ModerateurResource
    {
        public int IdModerateur { get; set; }  // Identifiant propre au modérateur

        // Si le modérateur est éventuellement lié à un événement pour la validation,
        // cette propriété est optionnelle.
        public int? IdEvenement { get; set; }

        // Optionnel : inclure les détails de l'événement associé
        // Si tu ne souhaites pas renvoyer l'événement complet, tu peux omettre cette propriété.
        public EvenementResource Evenement { get; set; }

        public string StatutValidation { get; set; }
        public DateTime DateValidation { get; set; }

    }
}
