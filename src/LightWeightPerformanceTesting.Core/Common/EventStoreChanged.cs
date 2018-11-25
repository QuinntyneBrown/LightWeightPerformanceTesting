using LightWeightPerformanceTesting.Core.Models;

namespace LightWeightPerformanceTesting.Core.Common
{
    public class EventStoreChanged
    {
        public EventStoreChanged(StoredEvent @event) => Event = @event;
        public StoredEvent Event { get; }
    }
}
