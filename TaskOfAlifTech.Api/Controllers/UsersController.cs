using Microsoft.AspNetCore.Mvc;
using TaskOfAlifTech.Domain.Configurations;
using TaskOfAlifTech.Service.DTOs.UserForCreation;
using TaskOfAlifTech.Service.Interfaces;

namespace TaskOfAlifTech.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await userService.GetAllAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await userService.GetAsync(p => p.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> AddAsync(UserForCreationDto dto)
            => Ok(await userService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute(Name = "Id")] long id, UserForCreationDto dto)
            => Ok(await userService.UpdateAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute(Name = "Id")] long id)
            => Ok(await userService.DeleteAsync(p => p.Id == id));

    }
}
