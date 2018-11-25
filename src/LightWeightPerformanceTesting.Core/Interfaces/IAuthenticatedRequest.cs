using MediatR;
using System;

namespace LightWeightPerformanceTesting.Core.Interfaces
{
    public interface IAuthenticatedRequest
    {
        Guid CurrentUserId { get; set; }
    }
}
