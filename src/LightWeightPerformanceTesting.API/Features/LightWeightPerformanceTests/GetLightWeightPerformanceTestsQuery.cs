using LightWeightPerformanceTesting.Core.Interfaces;
using LightWeightPerformanceTesting.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LightWeightPerformanceTesting.API.Features.LightWeightPerformanceTests
{
    public class GetLightWeightPerformanceTestsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<LightWeightPerformanceTestDto> LightWeightPerformanceTests { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
			public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    LightWeightPerformanceTests = _repository.Query<LightWeightPerformanceTest>().Select(x => LightWeightPerformanceTestDto.FromLightWeightPerformanceTest(x)).ToList()
                });
        }
    }
}
