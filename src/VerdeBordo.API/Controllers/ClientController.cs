using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using VerdeBordo.API.Controllers.Base;
using VerdeBordo.API.Controllers.Responses;
using VerdeBordo.Application.Features.Clients.Commands.DeleteClient;
using VerdeBordo.Application.Features.Clients.Commands.PostClient;
using VerdeBordo.Application.Features.Clients.Commands.UpdateClient;
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

        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <returns>Cliente criado</returns>
        /// <param name="command">Objeto contendo nome e contato do cliente a ser criado</param>
        /// <response code="201">Cliente criado com sucesso</response>
        /// <response code="400">Informações inválidas</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostClientAsync([FromBody] PostClientCommand command)
        {
            var clientId = await _mediator.Send(command);

            if (clientId is null)
                return CreateCustomResponse<BadRequestResponse>(clientId.HasValue);

            return CreatedAtAction(nameof(GetById), new {ClientId = clientId}, command);
        }

        /// <summary>
        /// Deleta logicamente um cliente
        /// </summary>
        /// <returns></returns>
        /// <param name="clientId">Id do cliente a ser deletado</param>
        /// <response code="204">Cliente deletado com sucesso</response>
        /// <response code="400">Cliente não encontrado ou já havia sido deletado </response>
        [HttpDelete("{clientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        public async Task<IActionResult> DeleteClientAsync(int clientId)
        {
            var result = await _mediator.Send(new DeleteClientCommand(clientId));

            return CreateCustomResponse<NoContentResponse>(result);
        }

        /// <summary>
        /// Atualiza um cliente
        /// </summary>
        /// <returns></returns>
        /// <param name="clientId">Id do cliente a ser atualizado</param>
        /// <param name="command">Objeto contendo novo nome e/ou novoc contato do cliente</param>
        /// <response code="204">Cliente atualizado com sucesso</response>
        /// <response code="400">Cliente não encontrado ou objeto para atualizaçao inválido </response>
        [HttpPut("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClientAsync(int clientId, [FromBody] UpdateClientCommand command)
        {
            command.ClientId = clientId;
            var result = await _mediator.Send(command);

            if (result is null)
                return CreateCustomResponse<BadRequestResponse>(clientId);

            return CreateCustomResponse<SuccessResponse>(result);
        }      
    }
}