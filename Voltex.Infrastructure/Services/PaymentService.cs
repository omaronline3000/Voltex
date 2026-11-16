using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;
using Voltex.Core.Interfaces;
using Voltex.Core.Services;
using Voltex.Infrastructure.Data;
using Stripe;
using Microsoft.EntityFrameworkCore;
namespace Voltex.Infrastructure.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _context = context;
        }

        public async Task<Cart> CreateOrUpdatePaymentAsync(string cartId, int? deliveryMethodId)
        {
            var Cart = await _unitOfWork.cartRepository.GetCartAsync(cartId);
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            decimal ShippingPrice = 0m;
            if (deliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _context.deliveryMethods.AsNoTracking().FirstOrDefaultAsync(d => d.Id == deliveryMethodId);
                ShippingPrice = DeliveryMethod.Price;
            }
            foreach (var item in Cart.Items)
            {
                var product = await _unitOfWork.productRepository.GetByIdAsync(item.Id);
                item.Price = product.NewPrice;
            }
            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent _intent;
            if (string.IsNullOrEmpty(Cart.PaymentIntentId))
            {
                PaymentIntentCreateOptions options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Cart.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(ShippingPrice * 100),
                    Currency = "USD",
                    AllowedPaymentMethodTypes = new List<string> { "Card"}
                };
                _intent = await paymentIntentService.CreateAsync(options);
                Cart.PaymentIntentId = _intent.Id;
                Cart.ClientSecret = _intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Cart.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(ShippingPrice * 100),
                };
                await paymentIntentService.UpdateAsync(Cart.PaymentIntentId, options);
            }
            await _unitOfWork.cartRepository.UpdateCartAsync(Cart);
            return Cart;
        }
    }
}
