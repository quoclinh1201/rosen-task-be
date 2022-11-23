using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.ISerices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetOwnProfile([FromHeader] string authorization)
        {
            var response = await _service.GetOwnProfile(authorization.Substring(7));
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        public async Task<IActionResult> UpdateOwnProfile([FromHeader] string authorization, [FromBody] UpdateProfileRequest request)
        {
            var response = await _service.UpdateOwnProfile(authorization.Substring(7), request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("change-avatar")]
        public async Task<IActionResult> ChangeAvatar([FromHeader] string authorization, IFormFile image)
        {
            var response = await _service.ChangeAvatar(authorization.Substring(7), image);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
