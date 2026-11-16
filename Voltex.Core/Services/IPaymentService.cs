using System;
using System.Collections.Generic;
using System.Text;
using Voltex.Core.Entities;

namespace Voltex.Core.Services
{
    public interface IPaymentService
    {
        Task<Cart> CreateOrUpdatePaymentAsync(string cartId, int? deliveryMethodId);
    }
}
