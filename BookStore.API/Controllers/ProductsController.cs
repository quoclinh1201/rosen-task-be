using BookStore.Business.Dto.Parameters;
using BookStore.Business.ISerices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetListProduct([FromQuery] GetListProductParameter parameter)
        {
            var response = await _service.GetListProduct(parameter);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailProduct(int id)
        {
            var response = await _service.GetDetailProduct(id);
            if (!response.IsSuccess)
            {
                if(response.Error.Code == 404)
                    return NotFound(response);
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}