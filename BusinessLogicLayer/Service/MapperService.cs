using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntitiesLayer.Entities;
using ViewModelsLayer.ViewModels;

namespace BusinessLogicLayer.Service
{
    public class MapperService : Profile
    {
        public static void Initialize()
        {
            new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Card, CardViewModel>()
                .ForMember(x => x.CardName, x => x.MapFrom(m => m.CardName))
                .ForMember(x => x.CardValue, x => x.MapFrom(m => m.CardValue))
                .ForMember(x => x.Suit, x => x.MapFrom(m => m.Suit));

                configuration.CreateMap<Game, GameViewModel>()
                .ForMember(x => x.Deck, x => x.MapFrom(m => m.Deck))
                .ForMember(x => x.DiscardPile, x => x.MapFrom(m => m.DiscardPile))
                .ForMember(x => x.Rounds, x => x.MapFrom(m => m.Rounds))
                .ForMember(x => x.Users, x => x.MapFrom(m => m.Users));

                configuration.CreateMap<Move, MoveViewModel>()
                .ForMember(x => x.User, x => x.MapFrom(m => m.User))
                .ForMember(x => x.Cards, x => x.MapFrom(m => m.Cards))
                .ForMember(x => x.IsWin, x => x.MapFrom(m => m.IsWin));

                configuration.CreateMap<Round, RoundViewModel>()
                .ForMember(x => x.Moves, x => x.MapFrom(m => m.Moves));

                configuration.CreateMap<User, UserViewModel>()
                .ForMember(x => x.Nickname, x => x.MapFrom(m => m.Nickname))
                .ForMember(x => x.UserRole, x => x.MapFrom(m => m.UserRole))
                .ForMember(x => x.Cards, x => x.MapFrom(m => m.Cards));
            });
        }
    }
}
