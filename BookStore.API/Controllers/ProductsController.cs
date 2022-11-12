using BookStore.Business.Dto.Parameters;
using BookStore.Business.ISerices;
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

        [HttpGet]
        public async Task<IActionResult> GetListProduct([FromQuery] GetListProductParameter parameter)
        {
            var result = await _service.GetListProduct(parameter);
            return Ok(result);
        }
    }
}
