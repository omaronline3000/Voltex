using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Voltex.API.Helper;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;

namespace Voltex.API.Controllers
{
    public class CartController : BaseController
    {
        public CartController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var cart = await _unitOfWork.cartRepository.GetCartAsync(id);
            if(cart is null)
            {
                cart = new Cart();
                return Ok(new ResponseApi<Cart>(message: "Data Returned Successfully", data: cart));
            }
            return Ok(new ResponseApi<Cart>(message: "Data Returned Successfully" , data: cart));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(Cart cart)
        {
            var res = await _unitOfWork.cartRepository.UpdateCartAsync(cart);
            return res is null ? throw new Exception() : (IActionResult)Ok(new ResponseApi<Cart>(message: "Data Saved Successfully", data: cart));
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delet(string id)
        {
            var cart = await _unitOfWork.cartRepository.DeleteCartAsync(id);
            if (!cart)
            {
                return NotFound(new ProblemDetails()
                {
                    Status = 404,
                    Title = "Not Found",
                    Detail = "Not Found Cart with that Id"
                });
            }
            return NoContent();
        }
    }
}
