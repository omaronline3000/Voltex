using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.DTOs;
using Voltex.Core.Entities;
using Voltex.Core.Entities.Order;

namespace Voltex.Core.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(AddOrderDTO orderDTO , string buyerEmail);
        Task<IReadOnlyList<OrderDTO>> GetAllOrdersAsync(string buyerMail);
        Task<OrderDTO> GetOrderByIdAsync(int id, string buyerMail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
