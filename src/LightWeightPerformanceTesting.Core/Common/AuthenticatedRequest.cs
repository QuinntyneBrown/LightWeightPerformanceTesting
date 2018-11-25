using LightWeightPerformanceTesting.Core.Interfaces;
using MediatR;
using System;

namespace LightWeightPerformanceTesting.Core.Common
{
    public class AuthenticatedRequest<TResponse> : IAuthenticatedRequest, IRequest<TResponse>
    {
        public Guid CurrentUserId { get; set; }
    }
}
