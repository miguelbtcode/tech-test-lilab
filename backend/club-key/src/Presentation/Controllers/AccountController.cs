namespace Presentation.Controllers;

using System.Net;
using System.Security.Claims;
using Application.Features.Auth.Users.Commands.LoginUser;
using Application.Features.Auth.Users.Commands.RegisterUser;
using Application.Features.Auth.Users.Queries.GetUserById;
using Application.Features.Auth.Users.Vms;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Inicia sesión en el sistema y obtiene un token de autenticación.
    /// </summary>
    /// <param name="command">Credenciales del usuario para autenticación.</param>
    /// <returns>Token de acceso y datos del usuario autenticado.</returns>
    /// <response code="200">Inicio de sesión exitoso.</response>
    /// <response code="401">Credenciales inválidas.</response>
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(typeof(AuthResponseVm), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<AuthResponseVm>> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }
        
        return Ok(result.Value);
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="command">Datos del usuario a registrar.</param>
    /// <returns>Información del usuario autenticado después del registro.</returns>
    /// <response code="200">Usuario registrado exitosamente.</response>
    /// <response code="400">Error en la solicitud, datos inválidos o usuario existente.</response>
    [Authorize(Roles = "Admin")]
    [HttpPost("Register", Name = "Register")]
    [ProducesResponseType(typeof(AuthResponseVm), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<AuthResponseVm>> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet("profile")]
    [ProducesResponseType(typeof(AuthResponseVm), StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Token inválido o expirado" });
        }
        
        var user = await _mediator.Send(new GetUserByIdQuery(userId));

        if (user.IsFailure)
        {
            return Unauthorized(user.Error);
        }

        return Ok(user.Value);
    }
}