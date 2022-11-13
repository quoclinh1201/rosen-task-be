using BookStore.Business.Dto.Parameters;
using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.ISerices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById([FromHeader] string authorization, int id)
        {
            var response = await _service.GetOrderDetailById(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetListOrders([FromHeader] string authorization, [FromQuery] QueryStringParameters param)
        {
            var response = await _service.GetListOrders(authorization.Substring(7), param);
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromHeader] string authorization, [FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _service.CreateOrder(authorization.Substring(7), request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder([FromHeader] string authorization, int id)
        {
            var response = await _service.CancelOrder(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("{id}")]
        public async Task<IActionResult> ReOrder([FromHeader] string authorization, int id)
        {
            var response = await _service.ReOrder(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
