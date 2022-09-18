using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.Application.Features.Clients.Queries.GetAllClients;
using VerdeBordo.Controllers.Responses;

namespace VerdeBordo.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/clients")]
    [OpenApiTag("Client", Description = "Clientes")]
    public class ClientController : BaseController
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        /// <returns>Clientes cadastrados</returns>
        /// <response code="200">Retorna os clientes cadastrados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var clients = await _mediator.Send(new GetAllClientsQuery());

            return CreateCustomResponse<SuccessResponse>(clients);
        }
    }
}