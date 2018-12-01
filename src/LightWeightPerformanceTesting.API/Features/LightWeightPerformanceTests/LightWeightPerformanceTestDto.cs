using LightWeightPerformanceTesting.Core.Models;
using System;

namespace LightWeightPerformanceTesting.API.Features.LightWeightPerformanceTests
{
    public class LightWeightPerformanceTestDto
    {        
        public Guid LightWeightPerformanceTestId { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static LightWeightPerformanceTestDto FromLightWeightPerformanceTest(LightWeightPerformanceTest lightWeightPerformanceTest)
            => new LightWeightPerformanceTestDto
            {
                LightWeightPerformanceTestId = lightWeightPerformanceTest.LightWeightPerformanceTestId,
                Selector = lightWeightPerformanceTest.Selector,
                Name = lightWeightPerformanceTest.Name,
                Description = lightWeightPerformanceTest.Description,
            };
    }
}
