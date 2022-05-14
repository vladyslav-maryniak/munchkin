﻿namespace Munchkin.API.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public int TurnIndex { get; set; }
        public GameLobbyDto Lobby { get; set; } = new();
        public TableDto Table { get; set; } = new();
    }
}
