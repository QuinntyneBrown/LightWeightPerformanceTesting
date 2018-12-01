using LightWeightPerformanceTesting.Core.Common;
using LightWeightPerformanceTesting.Core.DomainEvents;
using System;

namespace LightWeightPerformanceTesting.Core.Models
{
    public class LightWeightPerformanceTest: Entity
    {
        public LightWeightPerformanceTest(string name, string selector, string description)
            => Apply(new LightWeightPerformanceTestCreated(LightWeightPerformanceTestId, name, selector, description));

        public Guid LightWeightPerformanceTestId { get; set; } = Guid.NewGuid();          
		public string Selector { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LightWeightPerformanceTestStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case LightWeightPerformanceTestCreated lightWeightPerformanceTestCreated:
                    LightWeightPerformanceTestId = lightWeightPerformanceTestCreated.LightWeightPerformanceTestId;
                    Name = lightWeightPerformanceTestCreated.Name;
                    Selector = lightWeightPerformanceTestCreated.Selector;
                    Description = lightWeightPerformanceTestCreated.Description;
					Status = LightWeightPerformanceTestStatus.Active;
                    break;
            }
        }
        
    }

    public enum LightWeightPerformanceTestStatus
    {
        Active,
        InActive
    }
}
