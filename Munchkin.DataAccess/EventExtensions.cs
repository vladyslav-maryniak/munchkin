using EventStore.Client;
using Munchkin.Infrastucture.Events;

namespace Munchkin.DataAccess
{
    internal static class EventExtensions
    {
        internal static string BuildTypeName(this ResolvedEvent resolvedEvent)
        {
            var type = typeof(EventEntrypoint);
            return $"{type.Namespace}.{resolvedEvent.Event.EventType}, {type.Assembly}";
        }

        internal static string BuildStreamId(this Guid gameId) => $"MunchkinGameStream_{gameId}";
    }
}
