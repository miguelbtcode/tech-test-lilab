namespace Presentation.Controllers;

using System.Net;
using Application.Features.Auth.Roles.Commands.ChangeUserRole;
using Application.Features.Auth.Users.Commands.ChangeUserPassword;
using Application.Features.Auth.Users.Commands.DeleteUser;
using Application.Features.Auth.Users.Commands.RegisterUser;
using Application.Features.Auth.Users.Commands.UpdateUser;
using Application.Features.Auth.Users.Queries.GetUserById;
using Application.Features.Auth.Users.Vms;
using Application.Features.Shared.Queries.Vms;
using Application.Features.Users.Queries.GetAllUsersPagination;
using Domain.Abstractions;
using Domain.Roles;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Obtiene una lista paginada de usuarios.
    /// </summary>
    /// <param name="query">Parámetros de paginación y filtrado.</param>
    /// <returns>Lista paginada de usuarios.</returns>
    /// <response code="200">Retorna la lista paginada de usuarios.</response>
    /// <response code="400">Error en la solicitud.</response>
    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("pagination", Name = "PaginationUser")]
    [ProducesResponseType(typeof(PaginationVm<User>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<PaginationVm<User>>> GetUserList([FromQuery] GetAllUsersPaginationQuery query)
    {
        var resultPaginationUsers = await _mediator.Send(query);

        if (resultPaginationUsers.IsFailure)
        {
            return BadRequest(resultPaginationUsers.Error);
        }
        
        return Ok(resultPaginationUsers.Value);
    }
    
    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="command">Datos del usuario a registrar.</param>
    /// <response code="201">Usuario registrado correctamente.</response>
    /// <response code="400">Error en la solicitud, datos inválidos o usuario existente.</response>
    [Authorize(Roles = Role.ADMIN)]
    [HttpPost("register", Name = "RegisterUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var resultCommand = await _mediator.Send(command);

        if (resultCommand.IsFailure)
        {
            return BadRequest(resultCommand.Error);
        }

        return CreatedAtRoute("GetUserById", new { userId = resultCommand.Value }, resultCommand.Value);
    }
    
    /// <summary>
    /// Obtiene la información de un usuario por su ID.
    /// </summary>
    /// <param name="userId">Identificador único del usuario.</param>
    /// <returns>Información del usuario autenticado.</returns>
    /// <response code="200">Usuario encontrado correctamente.</response>
    /// <response code="400">Error en la solicitud, usuario no encontrado o ID inválido.</response>
    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("{userId}", Name = "GetUserById")]
    [ProducesResponseType(typeof(AuthResponseVm), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseVm>> GetUserById(string? userId)
    {
        var query = new GetUserByIdQuery(userId);
        var resultQuery = await _mediator.Send(query);

        if (resultQuery.IsFailure)
        {
            return BadRequest(resultQuery.Error);
        }
    
        return Ok(resultQuery.Value);
    }
    
    /// <summary>
    /// Actualiza la información de un usuario existente.
    /// </summary>
    /// <param name="command">Datos actualizados del usuario.</param>
    /// <returns>Información del usuario actualizado.</returns>
    /// <response code="200">Usuario actualizado correctamente.</response>
    /// <response code="400">Error en la solicitud, datos inválidos o usuario no encontrado.</response>
    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("Update", Name = "UpdateUser")]
    [ProducesResponseType(typeof(AuthResponseVm), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseVm>> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var resultCommand = await _mediator.Send(command);

        if (resultCommand.IsFailure)
        {
            return BadRequest(resultCommand.Error);
        }
        
        // Update role and password in parallel process
        var roleTask = _mediator.Send(new ChangeUserRoleCommand(command.Id, command.Role));
        Task<Result<bool>>? passwordTask = null;

        if (!string.IsNullOrWhiteSpace(command.Password))
        {
            passwordTask = _mediator.Send(new ChangeUserPasswordCommand(command.Id, command.Password));
        }
        
        var tasks = passwordTask != null ? new[] { roleTask, passwordTask } : new[] { roleTask };
        var results = await Task.WhenAll(tasks);
        
        var roleResult = results[0];
        if (roleResult.IsFailure)
            return BadRequest(roleResult.Error);

        if (passwordTask == null)
            return Ok(resultCommand.Value);

        var passwordResult = results[1];
        if (passwordResult.IsFailure)
            return BadRequest(passwordResult.Error);

        return Ok(resultCommand.Value);
    }
    
    /// <summary>
    /// Elimina un usuario del sistema.
    /// </summary>
    /// <param name="command">Comando con la información del usuario a eliminar.</param>
    /// <response code="204">Usuario eliminado correctamente, sin contenido en la respuesta.</response>
    /// <response code="400">Error en la solicitud, usuario no encontrado o datos inválidos.</response>
    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("Delete", Name = "DeleteUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand command)
    {
        var resultCommand = await _mediator.Send(command);

        if (resultCommand.IsFailure)
        {
            return BadRequest(resultCommand.Error);
        }
    
        return NoContent();
    }
}