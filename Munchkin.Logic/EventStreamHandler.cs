﻿using EventStore.Client;
using Munchkin.DataAccess;
using Munchkin.Infrastucture.Events.Base;

namespace Munchkin.Logic
{
    public class EventStreamHandler : IDisposable
    {
        private readonly IEventService service;
        private readonly IEventRepository repository;
        private StreamSubscription? subscription;
        
        private EventStreamHandler(IEventService service, IEventRepository repository)
        {
            this.service = service;
            this.repository = repository;
        }

        private async Task<EventStreamHandler> InitializeAsync()
        {
            subscription = await service.SubscribeAsync(EventAppeared);
            return this;
        }

        public static Task<EventStreamHandler> CreateAsync(IEventService service, IEventRepository repository)
        {
            var instance = new EventStreamHandler(service, repository);
            return instance.InitializeAsync();
        }

        private void EventAppeared(IGameEvent @event)
        {
            var game = repository.GetGame(@event.GameId);
            @event.Apply(game);
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
    }
}
