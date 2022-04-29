using AutoMapper;
using Munchkin.API.DTOs;
using Munchkin.Infrastucture.Cards.Base;
using Munchkin.Infrastucture.Projections;

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
