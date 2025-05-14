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
            CreateMap<EvenementResource, Evenement>()
                // Ignorer les propriétés non modifiables pour ne pas écraser l'organisateur et l'état
                .ForMember(dest => dest.IdOrganisateur, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreation, opt => opt.Ignore())
                .ForMember(dest => dest.Etat, opt => opt.Ignore())
                .ForMember(dest => dest.DateValidation, opt => opt.Ignore());

            // Participation
            CreateMap<Participation, ParticipationResource>();
            CreateMap<ParticipationResource, Participation>();

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

            // Statistique (optionnel)
            CreateMap<Statistique, StatistiqueResource>();
            CreateMap<StatistiqueResource, Statistique>();
        }
    }
}
