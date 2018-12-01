using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LightWeightPerformanceTesting.API.Features.LightWeightPerformanceTests
{
    [Authorize]
    [ApiController]
    [Route("api/lightWeightPerformanceTests")]
    public class LightWeightPerformanceTestsController
    {
        private readonly IMediator _mediator;

        public LightWeightPerformanceTestsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateLightWeightPerformanceTestCommand.Response>> Create(CreateLightWeightPerformanceTestCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateLightWeightPerformanceTestCommand.Response>> Update([FromBody]UpdateLightWeightPerformanceTestCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{lightWeightPerformanceTestId}")]
        public async Task Remove([FromRoute]RemoveLightWeightPerformanceTestCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{lightWeightPerformanceTestId}")]
        public async Task<ActionResult<GetLightWeightPerformanceTestByIdQuery.Response>> GetById([FromRoute]GetLightWeightPerformanceTestByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetLightWeightPerformanceTestsQuery.Response>> Get()
            => await _mediator.Send(new GetLightWeightPerformanceTestsQuery.Request());
    }
}
