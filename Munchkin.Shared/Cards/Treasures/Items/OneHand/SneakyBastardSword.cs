﻿using Munchkin.Shared.Cards.Base.Treasures.Items;

namespace Munchkin.Shared.Cards.Treasures.Items.OneHand
{
    public class SneakyBastardSword : OneHandCard
    {
        public override Guid Id { get; set; }
        public override int Bonus => 2;
        public override string Name => "Sneaky bastard sword";
        public override string Description => string.Empty;
        public override int GoldPieces => 400;

        public SneakyBastardSword()
        {
            Id = Guid.NewGuid();
        }
    }
}