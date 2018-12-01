using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightWeightPerformanceTesting.API.Features.LightWeightPerformanceTests
{
    public class GetLightWeightPerformanceTestByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.LightWeightPerformanceTestId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid LightWeightPerformanceTestId { get; set; }
        }

        public class Response
        {
            public LightWeightPerformanceTestDto LightWeightPerformanceTest { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
			public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    LightWeightPerformanceTest = LightWeightPerformanceTestDto.FromLightWeightPerformanceTest(_repository.Query<LightWeightPerformanceTest>().Single(x => x.LightWeightPerformanceTestId == request.LightWeightPerformanceTestId))
                });
        }
    }
}
