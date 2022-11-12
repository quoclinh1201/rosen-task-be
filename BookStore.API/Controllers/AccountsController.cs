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
        private readonly IAccountService service;


        public AccountsController(IAccountService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await service.Login(request);
            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
