using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.ISerices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _service;

        public CartsController(ICartService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetCart([FromHeader] string authorization)
        {
            var response = await _service.GetCart(authorization.Substring(7));
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromHeader] string authorization, [FromBody] AddToCartRequest request)
        {
            var response = await _service.AddToCart(authorization.Substring(7), request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("incease-product/{id}")]
        public async Task<IActionResult> IncreaseProduct([FromHeader] string authorization, int id)
        {
            var response = await _service.IncreaseProduct(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("decease-product/{id}")]
        public async Task<IActionResult> DecreaseProduct([FromHeader] string authorization, int id)
        {
            var response = await _service.DecreaseProduct(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("remove-product/{id}")]
        public async Task<IActionResult> RemoveProduct([FromHeader] string authorization, int id)
        {
            var response = await _service.RemoveProduct(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
