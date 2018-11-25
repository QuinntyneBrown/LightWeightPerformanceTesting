using System;

namespace LightWeightPerformanceTesting.Core.DomainEvents
{
    public class RoleNameChanged: DomainEvent
    {
        public RoleNameChanged(string name)
        {
             Name = name;
        }

        public string Name { get; set; }
    }
}
