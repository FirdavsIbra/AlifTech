using Microsoft.AspNetCore.Mvc;
using TaskOfAlifTech.Service.DTOs.Users;
using TaskOfAlifTech.Service.Interfaces;

namespace TaskOfAlifTech.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("Login")]
    public async ValueTask<IActionResult> Login(UserForLoginDto dto)
    {
        var token = await authService.GenerateTokenAsync(dto.Login, dto.Password);

        return Ok(new
        {
            Token = token
        });
    }
}

