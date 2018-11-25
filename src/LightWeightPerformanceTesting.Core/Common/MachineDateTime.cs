using LightWeightPerformanceTesting.Core.Interfaces;
using System;

namespace LightWeightPerformanceTesting.Core.Common
{
    public class MachineDateTime : IDateTime
    {
        public DateTime UtcNow { get { return System.DateTime.UtcNow; } }
    }
}
