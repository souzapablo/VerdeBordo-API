using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Application.Features.Clients.Queries.GetAllClients;
using VerdeBordo.Application.Features.Clients.Queries.GetClientById;
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

        /// <summary>
        /// Busca o cliente pelo Id
        /// </summary>
        /// <returns>Detalhes do cliente</returns>
        /// <param name="clientId">Id do cliente a ser encontrado</param>
        /// <response code="200">Retorna os detalhes do cliente encontrado</response>
        /// <response code="404">Cliente com o id informado não encontrado</response>
        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int clientId)
        {
            var client = await _mediator.Send(new GetClientByIdQuery(clientId));

            if (client is null)
                return CreateCustomResponse<NotFoundResponse>(clientId);
            
            return CreateCustomResponse<SuccessResponse>(client);
        }
    }
}