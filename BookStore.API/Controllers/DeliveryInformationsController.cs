using BookStore.Business.Dto.RequestObjects;
using BookStore.Business.ISerices;
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/delivery-information")]
    [ApiController]
    public class DeliveryInformationsController : ControllerBase
    {
        private readonly IDeliveryInformaionService _service;

        public DeliveryInformationsController(IDeliveryInformaionService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetListDeliveryInformation([FromHeader] string authorization)
        {
            var response = await _service.GetListDeliveryInformation(authorization.Substring(7));
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateDeliveryInformation([FromHeader] string authorization, [FromBody] CreateDeliveryInformationRequest request)
        {
            var response = await _service.CreateDeliveryInformation(authorization.Substring(7), request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryInformation([FromHeader] string authorization, int id)
        {
            var response = await _service.DeleteDeliveryInformation(authorization.Substring(7), id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
