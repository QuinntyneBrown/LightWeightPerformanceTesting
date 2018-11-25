using System;
using System.Collections.Generic;

namespace LightWeightPerformanceTesting.Core.Identity
{
    public interface ISecurityTokenFactory
    {
        string Create(Guid userId, string uniqueName, IEnumerable<string> roles = null);
    }
}
