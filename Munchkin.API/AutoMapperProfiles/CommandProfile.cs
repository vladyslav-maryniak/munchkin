﻿using AutoMapper;
using Munchkin.API.DTOs;
using Munchkin.Shared.Cards.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Cards.Base.Treasures.Items;
using Munchkin.Shared.Models;

namespace Munchkin.API.AutoMapperProfiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Table, TableDto>();
            CreateMap<Place, PlaceDto>();
            CreateMap<CombatField, CombatFieldDto>();
            CreateMap<Equipment, EquipmentDto>();
            
            CreateMap<MunchkinCard, MunchkinCardDto>();
            CreateMap<MonsterCard, MonsterCardDto>();

            CreateMap<HeadgearCard, ItemCardDto>();
            CreateMap<ArmorCard, ItemCardDto>();
            CreateMap<FootgearCard, ItemCardDto>();
            CreateMap<HandCard, ItemCardDto>();

            CreateMap<CharacterDto, Character>().ReverseMap();
            CreateMap<PlayerDto, Player>().ReverseMap();
        }
    }
}
