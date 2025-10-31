using BichoBet.API.Contracts;
using BichoBet.Application.Handlers.User.CreateAdmin;
using BichoBet.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BichoBet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] CreateAdminCommand req)
    {
        var res = await mediator.Send(req);
        if (!res.Succeeded)
            return BadRequest(
                ApiResponse<object>.FromErrors(res.Errors.Select(e => new ErrorItem(e.Code, null, e.Description))));

        return Created("api/auth/register", new { Message = "User registered successfully" });
    }
}