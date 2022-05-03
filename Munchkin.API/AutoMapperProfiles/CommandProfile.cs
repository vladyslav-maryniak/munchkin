using AutoMapper;
using Munchkin.API.DTOs;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.API.AutoMapperProfiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Table, TableDto>();
            CreateMap<MonsterCard, MonsterCardDto>();

            CreateMap<CharacterDto, Character>().ReverseMap();
            CreateMap<PlayerDto, Player>().ReverseMap();
        }
    }
}
