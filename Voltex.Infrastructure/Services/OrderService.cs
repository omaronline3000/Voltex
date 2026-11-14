using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;
using Voltex.Core.Entities.Order;
using Voltex.Core.Interfaces;
using Voltex.Core.Services;
using Voltex.Infrastructure.Data;

namespace Voltex.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public OrderService(IMapper mapper, AppDbContext context, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<OrderDTO> CreateOrderAsync(AddOrderDTO orderDTO , string buyerEmail)
        {
            var Cart = await _unitOfWork.cartRepository.GetCartAsync(orderDTO.CartId);
            var OrderItems = new List<OrderItem>();

            foreach(var item in Cart.Items)
            {
                var product = await _unitOfWork.productRepository.GetByIdAsync(item.Id);
                var OrderItem = new OrderItem
                    (product.Id, item.ImageUrl, item.Name, item.Quantity, item.Price);
                OrderItems.Add(OrderItem);
            }

            decimal subtotal = OrderItems.Sum(o => o.Price * o.Quantity);

            var deliveryMethod = await _context.deliveryMethods.FindAsync(orderDTO.deliveryMethodId);
            var shippingData = _mapper.Map<ShippingData>(orderDTO.shipdata);
            var order = new Order(buyerEmail, subtotal, shippingData, deliveryMethod,OrderItems);
            await _unitOfWork.orderRepository.AddAsync(order);
            await _unitOfWork.cartRepository.DeleteCartAsync(orderDTO.CartId);
            var Orderdto = _mapper.Map<OrderDTO>(order);
            return Orderdto;

        }

        public async Task<IReadOnlyList<OrderDTO>> GetAllOrdersAsync(string buyerMail)
        => _mapper.Map<IReadOnlyList<OrderDTO>>(await _unitOfWork.orderRepository.GetAllAsync(buyerMail, o => o.DeliveryMethod, o => o.ShippingData));

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        => await _context.deliveryMethods.AsNoTracking().ToListAsync();
        

        public async Task<OrderDTO> GetOrderByIdAsync(int id, string buyerMail)
        => _mapper.Map<OrderDTO>(await _unitOfWork.orderRepository.GetByIdAsync(id,buyerMail, o => o.DeliveryMethod, o => o.ShippingData));

        
    }
}
