using System;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }         
    }
}
