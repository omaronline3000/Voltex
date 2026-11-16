using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Voltex.API.Helper;
using Voltex.Core.Entities;
using Voltex.Core.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Voltex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string CartId , int? deliveryId)
        {
           var cart = await _paymentService.CreateOrUpdatePaymentAsync(CartId, deliveryId);
            return StatusCode(statusCode: StatusCodes.Status201Created, new ResponseApi<Cart>(message: "Payment Data Created", data: cart));
        }

    }
}
