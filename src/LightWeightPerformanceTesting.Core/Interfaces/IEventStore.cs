using LightWeightPerformanceTesting.Core.Common;
using System;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface IEventStore : IDisposable
    {
        void Save(Entity aggregateRoot);
        TAggregateRoot Query<TAggregateRoot>(string propertyName, string value)
            where TAggregateRoot : Entity;
        TAggregateRoot Query<TAggregateRoot>(Guid id)
            where TAggregateRoot : Entity;
        TAggregateRoot[] Query<TAggregateRoot>()
            where TAggregateRoot : Entity;
    }
}
