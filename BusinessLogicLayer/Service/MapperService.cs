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
    public class MapperServiceProfile : Profile
    {
        public MapperServiceProfile() : base()
        {
            CreateMap<Card, CardViewModel>()
            .ForMember(x => x.CardName, x => x.MapFrom(m => m.CardName))
            .ForMember(x => x.CardValue, x => x.MapFrom(m => m.CardValue))
            .ForMember(x => x.Suit, x => x.MapFrom(m => m.Suit));

            CreateMap<Move, MoveViewModel>()
            .ForMember(x => x.User, x => x.MapFrom(m => m.User))
            .ForMember(x => x.Card, x => x.Ignore());

            CreateMap<User, UserViewModel>()
            .ForMember(x => x.Nickname, x => x.MapFrom(m => m.Nickname))
            .ForMember(x => x.UserRole, x => x.MapFrom(m => m.UserRole))
            .ForMember(x => x.Cards, x => x.MapFrom(m => m.Cards));
        }
    }
}
