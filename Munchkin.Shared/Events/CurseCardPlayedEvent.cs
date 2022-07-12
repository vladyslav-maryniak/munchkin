using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Events.Base;
using Munchkin.Shared.Models;

namespace Munchkin.Shared.Events
{
    public record CurseCardPlayedEvent(
        Guid GameId, Guid PlayerId, Guid CardId, Dictionary<string, string>? Metadata = default) : IGameEvent
    {
        public void Apply(Game game)
        {
            var place = game.Table.Places
                .First(x => x.Player.Id == PlayerId);
            var card = (CurseCard)place.InHandCards
                .First(x => x.Id == CardId);

            if (Metadata is not null
                && Metadata.TryGetValue("cursedCharacterId", out string? cursedCharacterIdMeta)
                && cursedCharacterIdMeta is not null
                && Guid.TryParse(cursedCharacterIdMeta, out Guid cursedCharacterId))
            {
                place.InHandCards.Remove(card);

                card.Apply(game, place.Character);

                if (place.Character.Level < 1)
                {
                    place.Character.Level = 1;
                }

                game.Table.Discard(card);
            }
        }
    }
}
