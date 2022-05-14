﻿using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record GameCreatedEvent(Guid GameId) : IGameEvent
    {
        public void Apply(Game game)
        {
        }
    }
}
