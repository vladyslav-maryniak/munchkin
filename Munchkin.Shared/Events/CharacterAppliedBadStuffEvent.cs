﻿using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Projections;

namespace Munchkin.Shared.Events
{
    public record CharacterAppliedBadStuffEvent(Guid GameId, Guid CharacterId) : IGameEvent
    {
        public void Apply(Game game)
        {
            var character = game.Characters.First(x => x.Id == CharacterId);
            game.Table.MonsterCards.ForEach(x => x.ApplyBadStuff(character));

            game.Table = new();
        }
    }
}
