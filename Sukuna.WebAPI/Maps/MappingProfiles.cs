using AutoMapper;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Maps
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Utilisateur
            CreateMap<Utilisateur, UtilisateurResource>();
            CreateMap<UtilisateurResource, Utilisateur>();

            // Evenement
            CreateMap<Evenement, EvenementResource>();
            CreateMap<EvenementResource, Evenement>();

            // Participation
            CreateMap<Participation, ParticipationResource>();
            CreateMap<ParticipationResource, Participation>();

            // Moderateur
            CreateMap<Moderateur, ModerateurResource>();
            CreateMap<ModerateurResource, Moderateur>();

            // Commentaire
            CreateMap<Commentaire, CommentaireResource>();
            CreateMap<CommentaireResource, Commentaire>();

            // Ressource
            CreateMap<Ressource, RessourceResource>();
            CreateMap<RessourceResource, Ressource>();

            // Badge
            CreateMap<Badge, BadgeResource>();
            CreateMap<BadgeResource, Badge>();

            // Interaction
            CreateMap<Interaction, InteractionResource>();
            CreateMap<InteractionResource, Interaction>();

            // Optionnel : Statistique, si besoin
            CreateMap<Statistique, StatistiqueResource>();
            CreateMap<StatistiqueResource, Statistique>();
        }
    }
}
