using AutoMapper;
using Munchkin.API.DTOs;
using Munchkin.Infrastucture.Projections;
using Munchkin.Logic.Commands;

namespace Munchkin.API.AutoMapperProfiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<JoinPlayerDto, JoinPlayer.Command>();
            CreateMap<PlayerDto, Player>();
        }
    }
}
