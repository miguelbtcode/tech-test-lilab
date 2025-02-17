namespace Presentation.Controllers;

using System.Net;
using Application.Features.Auth.Roles.Queries.GetAllRoles;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetRoles")]
    [ProducesResponseType(typeof(List<IdentityRole>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<IdentityRole>>> GetRoles()
    {
        var query = new GetAllRolesQuery();
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
            
        return Ok(result.Value);
    }
}
