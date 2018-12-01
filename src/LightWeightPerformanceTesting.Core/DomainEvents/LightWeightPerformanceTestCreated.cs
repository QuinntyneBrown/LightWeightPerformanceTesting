using System;
using System.Collections.Generic;
using System.Text;

namespace LightWeightPerformanceTesting.Core.DomainEvents
{
    public class LightWeightPerformanceTestCreated
    {
        public LightWeightPerformanceTestCreated(Guid lightWeightPerformanceTestId, string name, string selector, string description)
        {
            LightWeightPerformanceTestId = lightWeightPerformanceTestId;
            Name = name;
            Selector = selector;
            Description = description;
        }

        public string Name { get; set; }
        public string Selector { get; set; }
        public string Description { get; set; }
        public Guid LightWeightPerformanceTestId { get; set; }
    }
}
