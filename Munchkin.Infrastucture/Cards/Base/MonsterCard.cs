﻿using Munchkin.Infrastucture.Projections;

namespace Munchkin.Infrastucture.Cards.Base
{
    public abstract class MonsterCard : DoorCard
    {
        public abstract int Level { get; }
        public abstract string BadStuff { get; }
        public abstract int VictoryLevels { get; }
        public abstract void ApplyBadStuff(Character character);
    }
}