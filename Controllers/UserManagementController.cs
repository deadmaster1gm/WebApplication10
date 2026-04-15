using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication10.Contracts.DTO;
using WebApplication10.Entities;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers( [FromQuery] GetUsersQueryDto query, CancellationToken ct)
        {
            var users = await _userService.GetAllAsync(query, ct);

            return Ok(users);
        }

        [HttpPut("update/{id:guid}")]
        public async Task<ActionResult> UpdateUser (Guid id, UpdateRequestUserDto dto, CancellationToken ct)
        {
            var user = await _userService.UpdateAsync(id, dto, ct);

            if (!user)
                return NotFound();

            return Ok(user);
        }
        [HttpDelete("delete/{id:guid}")]
        public async Task <IActionResult> DeleteUser (Guid id, CancellationToken ct)
        {
            var user = await _userService.DeleteAsync(id, ct);

            if (!user)
                return NotFound();

            return NoContent();
        }
    }
}
