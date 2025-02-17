namespace Presentation.Controllers;

using System.Net;
using Application.Features.Customers.Commands.RegisterCustomer;
using Application.Features.Customers.Queries.GetAuditRegister;
using Application.Features.Customers.Queries.GetCustomerActivityPagination;
using Application.Features.Customers.Vms;
using Application.Features.Entrances.Commands.RegisterEntrance;
using Application.Features.Exits.Commands.RegisterExit;
using Application.Features.Shared.Queries.Vms;
using Domain.Abstractions;
using Domain.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Obtiene una lista paginada de clientes.
    /// </summary>
    /// <param name="query">Parámetros de paginación y filtrado.</param>
    /// <returns>Lista paginada de clientes.</returns>
    /// <response code="200">Retorna la lista paginada de clientes.</response>
    /// <response code="400">Error en la solicitud.</response>
    [Authorize(Roles = Role.USER)]
    [HttpGet("pagination", Name = "PaginationCustomer")]
    [ProducesResponseType(typeof(PaginationVm<CustomerVm>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<PaginationVm<CustomerVm>>> PaginationCustomer([FromQuery] GetAllCustomerPaginationQuery query)
    {
        var resultPaginationCustomers = await _mediator.Send(query);

        if (resultPaginationCustomers.IsFailure)
        {
            return BadRequest(resultPaginationCustomers.Error);
        }
        
        return Ok(resultPaginationCustomers.Value);
    }
    
    /// <summary>
    /// Obtiene una lista paginada de la actividad del cliente.
    /// </summary>
    /// <param name="query">Parámetros de paginación y filtrado.</param>
    /// <returns>Lista paginada de la actividad de un cliente.</returns>
    /// <response code="200">Retorna la lista paginada de la actividad de un cliente.</response>
    /// <response code="400">Error en la solicitud.</response>
    [Authorize(Roles = Role.USER)]
    [HttpGet("activity", Name = "PaginationCustomerActivity")]
    [ProducesResponseType(typeof(PaginationVm<CustomerActivityVm>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<PaginationVm<CustomerActivityVm>>> PaginationCustomerActivity([FromQuery] GetCustomerActivityPaginationQuery query)
    {
        var resultPaginationCustomers = await _mediator.Send(query);

        if (resultPaginationCustomers.IsFailure)
        {
            return BadRequest(resultPaginationCustomers.Error);
        }
        
        return Ok(resultPaginationCustomers.Value);
    }
    
    /// <summary>
    /// Registra un nuevo cliente en el sistema.
    /// </summary>
    /// <param name="command">Datos del cliente a registrar.</param>
    /// <response code="201">Cliente registrado correctamente.</response>
    /// <response code="400">Error en la solicitud, datos inválidos o cliente existente.</response>
    [Authorize(Roles = Role.USER)]
    [HttpPost("register", Name = "RegisterCustomer")]
    [ProducesResponseType(typeof(CustomerVm), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerVm>> RegisterCustomer([FromBody] RegisterCustomerCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(
            nameof(RegisterCustomer),
            new { id = result.Value.Id },
            result.Value);
    }
    
    /// <summary>
    /// Registra la entrada de un usuario en el sistema.
    /// </summary>
    /// <param name="command">Datos de la entrada a registrar.</param>
    /// <response code="201">Entrada registrada correctamente.</response>
    /// <response code="400">Error en la solicitud, datos inválidos o entrada duplicada.</response>
    [Authorize(Roles = Role.USER)]
    [HttpPost("entrance/register", Name = "RegisterEntrance")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterEntrance([FromBody] RegisterEntranceCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return StatusCode(StatusCodes.Status201Created);
    }
    
    /// <summary>
    /// Registra la salida de un usuario en el sistema.
    /// </summary>
    /// <param name="command">Datos de la salida a registrar.</param>
    /// <response code="201">Salida registrada correctamente.</response>
    /// <response code="400">Error en la solicitud, datos inválidos o salida duplicada.</response>
    [Authorize(Roles = Role.USER)]
    [HttpPost("exit/register", Name = "RegisterExit")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterExit([FromBody] RegisterExitCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return StatusCode(StatusCodes.Status201Created);
    }
}