using Eticket.Models;

namespace Eticket.Data.OrderServices
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string UserId,string Email);
        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string UserId,string UserRole);
    }
}
