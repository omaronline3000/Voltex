using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.DTOs;
using Voltex.Core.Entities.Order;

namespace Voltex.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderDTO orderDTO);
        Task<IReadOnlyList<Order>> GetAllOrdersAsync(string buyerMail);
        Task<Order> GetOrderById(string id, string buyerMail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods();
    }
}
