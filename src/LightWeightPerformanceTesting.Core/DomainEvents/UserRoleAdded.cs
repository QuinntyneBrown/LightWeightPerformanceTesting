using System;
using System.Collections.Generic;
using System.Text;

namespace LightWeightPerformanceTesting.Core.DomainEvents
{
    public class UserRoleAdded : DomainEvent
    {
        public UserRoleAdded(Guid roleId)
        {
            RoleId = roleId;
        }

        public Guid RoleId { get; set; }
    }
}
