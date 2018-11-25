using LightWeightPerformanceTesting.Core.Interfaces;
using System.Collections.Generic;

namespace LightWeightPerformanceTesting.Core.Common
{
    public class AuthenticatedCommand<TResponse> : AuthenticatedRequest<TResponse>, IAuthenticatedCommand<TResponse>
    {
        public string Key => "";

        public IEnumerable<string> SideEffects => new string[0];
    }
}
