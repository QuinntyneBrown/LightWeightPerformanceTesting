using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace LightWeightPerformanceTesting.API.Features.LightWeightPerformanceTests
{
    public class CreateLightWeightPerformanceTestCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.LightWeightPerformanceTest.LightWeightPerformanceTestId).NotNull();
            }
        }

        public class Request : IRequest<Response>
        {
            public LightWeightPerformanceTestDto LightWeightPerformanceTest { get; set; }
        }

        public class Response
        {
            public Guid LightWeightPerformanceTestId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var lightWeightPerformanceTest = new LightWeightPerformanceTest(
                    request.LightWeightPerformanceTest.Name, 
                    request.LightWeightPerformanceTest.Selector,
                    request.LightWeightPerformanceTest.Description);

                _eventStore.Save(lightWeightPerformanceTest);

                return Task.FromResult(new Response() { LightWeightPerformanceTestId = request.LightWeightPerformanceTest.LightWeightPerformanceTestId });
            }
        }
    }
}
