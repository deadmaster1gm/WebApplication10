using Microsoft.AspNetCore.Mvc;
using WebApplication10.Contracts.DTO;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers( [FromQuery] GetUsersQueryDto query, CancellationToken ct)
        {
            var users = await _userService.GetAllAsync(query, ct);
            return Ok(users);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser (Guid id, UpdateRequestUserDto dto, CancellationToken ct)
        {
            var user = await _userService.UpdateAsync(id, dto, ct);

            if (!user)
                return NotFound();

            return Ok(user);
        }
        [HttpDelete("{id:guid}")]
        public async Task <IActionResult> DeleteUser (Guid id, CancellationToken ct)
        {
            var user = await _userService.DeleteAsync(id, ct);

            if (!user)
                return NotFound();

            return NoContent();
        }
    }
}
