using LightWeightPerformanceTesting.Core.Models;
using System;

namespace LightWeightPerformanceTesting.API.Features.DashboardCards
{
    public class DashboardCardOptionsDto
    {        
        public int Top { get; set; } = 1;
        public int Left { get; set; } = 1;
        public int Height { get; set; } = 1;
        public int Width { get; set; } = 1;
    }

    public class DashboardCardLightWeightPerformanceTestOptionsDto: DashboardCardOptionsDto
    {        
        public string Selector { get; set; }
        public string BaseUrl { get; set; } 
        public string Username { get; set; }    
        public string Password { get; set; }
        public string PartitionKey { get; set; }
    }    
}
