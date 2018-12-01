using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace LightWeightPerformanceTesting.API.Features.LightWeightPerformanceTests
{
    public class RemoveLightWeightPerformanceTestCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.LightWeightPerformanceTestId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid LightWeightPerformanceTestId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var lightWeightPerformanceTest = _eventStore.Load<LightWeightPerformanceTest>(request.LightWeightPerformanceTestId);
                
                _eventStore.Save(lightWeightPerformanceTest);

                return Task.CompletedTask;
            }
        }
    }
}
