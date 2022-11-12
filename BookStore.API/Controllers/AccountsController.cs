using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.ISerices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;


        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _service.Login(request);
            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // Errorrrrrr ModelState
        [AllowAnonymous]
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _service.CreateAccount(request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
