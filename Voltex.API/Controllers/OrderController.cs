using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Voltex.API.Helper;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Order;
using Voltex.Core.Services;
namespace Voltex.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddOrderDTO orderdto)
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await _orderService.CreateOrderAsync(orderdto, email);
            return StatusCode(statusCode: StatusCodes.Status201Created, new { message = "Order Created Successfully", data = order });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await _orderService.GetAllOrdersAsync(email);
            return Ok(new ResponseApi<IReadOnlyList<OrderDTO>>(message: "Data Returned Successfully", data: order));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await _orderService.GetOrderByIdAsync(id, email);
            return Ok(new ResponseApi<OrderDTO>(message: "Data Returned Successfully", data: order));
        }
        [HttpGet("DeliverMethods")]
        public async Task<IActionResult> DeliveryMethods()
        {
            var delivery = await _orderService.GetDeliveryMethodsAsync();
            return Ok(new ResponseApi<IReadOnlyList<DeliveryMethod>>(message: "Data Returned Successfully", data: delivery));
        }
    }
}
