﻿using MongoDB.Bson.Serialization.Attributes;

namespace Munchkin.Shared.Models
{
    public class Character
    {
        [BsonId]
        public Guid Id { get; set; }
        public int Level { get; set; } = 1;
        public Equipment Equipment { get; set; } = new();

        public Character()
        {
            Id = Guid.NewGuid();
        }
    }
}
