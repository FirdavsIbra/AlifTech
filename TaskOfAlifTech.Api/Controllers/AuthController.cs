using Microsoft.AspNetCore.Mvc;
using TaskOfAlifTech.Service.DTOs.UserForCreation;
using TaskOfAlifTech.Service.Interfaces;

namespace TaskOfAlifTech.Api.Controllers
{
    public class AuthController : BaseController
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
}
