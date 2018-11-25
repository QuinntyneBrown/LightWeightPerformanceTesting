using LightWeightPerformanceTesting.Core.Common;
using LightWeightPerformanceTesting.Core.DomainEvents;
using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static LightWeightPerformanceTesting.Infrastructure.Data.DeserializedEventStore;
using static Newtonsoft.Json.JsonConvert;

namespace LightWeightPerformanceTesting.Infrastructure.Data
{
    public static class DeserializedEventStore
    {
        public static ConcurrentDictionary<Guid, DeserializedStoredEvent> Events { get; set; }
    }

    public class DeserializedStoredEvent
    {
        public DeserializedStoredEvent(StoredEvent @event)
        {
            StoredEventId = @event.StoredEventId;
            StreamId = @event.StreamId;
            Type = @event.Type;
            Aggregate = @event.Aggregate;
            Data = DeserializeObject(@event.Data, System.Type.GetType(@event.DotNetType));
            DotNetType = @event.DotNetType;
            CreatedOn = @event.CreatedOn;
            Version = @event.Version;
        }

        public Guid StoredEventId { get; set; }
        public Guid StreamId { get; set; }
        public string Type { get; set; }
        public string Aggregate { get; set; }
        public object Data { get; set; }
        public string DotNetType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Version { get; set; }
    }

    public class EventStore : IEventStore
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventStore(
            IAppDbContext context,
            IMediator mediator = default(IMediator),
            IBackgroundTaskQueue queue = default(IBackgroundTaskQueue),
            IServiceScopeFactory serviceScopeFactory = default(IServiceScopeFactory)
            )
        {
            _context = context;
            _mediator = mediator;
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Dispose() => _context.Dispose();

        public void Save(Entity entity)
        {
            var type = entity.GetType();

            foreach (var @event in entity.DomainEvents)
            {
                Add(new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = entity.GetType().Name,
                    Data = SerializeObject(@event),
                    StreamId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(entity, null),
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = DateTime.UtcNow
                });
            }

            entity.ClearChanges();
        }

        public T Query<T>(Guid id)
            where T : Entity
        {
            var list = new List<DomainEvent>();

            foreach (var storedEvent in Get().Where(x => x.StreamId == id))
                list.Add(storedEvent.Data as DomainEvent);

            return Load<T>(list.ToArray());
        }

        private T Load<T>(DomainEvent[] events)
            where T : Entity
        {
            var aggregate = (T)FormatterServices.GetUninitializedObject(typeof(T));

            foreach (var @event in events) aggregate.Apply(@event);

            aggregate.ClearChanges();

            return aggregate;
        }

        public TAggregateRoot Query<TAggregateRoot>(string propertyName, string value)
            where TAggregateRoot : Entity
        {
            var storedEvents = Get()
                .Where(x => {
                    var prop = Type.GetType(x.DotNetType).GetProperty(propertyName);
                    return prop != null && $"{prop.GetValue(x.Data, null)}" == value;
                })
                .ToArray();

            if (storedEvents.Length < 1) return null;

            return Query<TAggregateRoot>(storedEvents.First().StreamId) as TAggregateRoot;
        }

        public TAggregateRoot[] Query<TAggregateRoot>()
            where TAggregateRoot : Entity
        {
            var aggregates = new List<TAggregateRoot>();

            foreach (var grouping in Get()
                .Where(x => x.Aggregate == typeof(TAggregateRoot).Name).GroupBy(x => x.StreamId))
            {
                var events = grouping.Select(x => x.Data as DomainEvent).ToArray();

                aggregates.Add(Load<TAggregateRoot>(events.ToArray()));
            }

            return aggregates.ToArray();
        }

        protected List<DeserializedStoredEvent> Get()
        {
            if (Events == null)
                Events = new ConcurrentDictionary<Guid, DeserializedStoredEvent>(_context.StoredEvents.Select(x => new DeserializedStoredEvent(x)).ToDictionary(x => x.StoredEventId));

            return Events.Select(x => x.Value)
                .OrderBy(x => x.CreatedOn)
                .ToList();
        }

        protected void Add(StoredEvent @event)
        {
            Events.TryAdd(@event.StoredEventId, new DeserializedStoredEvent(@event));
            Persist(@event);
        }

        public void Persist(StoredEvent @event)
            => _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    context.StoredEvents.Add(@event);
                    context.SaveChanges();
                }

                await Task.CompletedTask;
            });
    }
}
