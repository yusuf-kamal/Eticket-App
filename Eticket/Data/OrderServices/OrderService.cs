using Eticket.Models;
using ETicket.data;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Data.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly EticketDbContext _context;

        public OrderService(EticketDbContext context )
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string UserId,string UserRole)
        {
            var order= await _context.Orders.Include(n=>n.OrderItems).ThenInclude(m=>m.Movie).Include(n=>n.User).ToListAsync();
            if (UserRole != "Admin")
            {
                order=order.Where(n=>n.UserId==UserId).ToList();
            }
            return order;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string Email)
        {
            var order = new Order()
            {
                UserId = UserId,
                Email = Email,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price,

                };
                await _context.OrderItems.AddAsync(orderitem);
            }
            await _context.SaveChangesAsync();

        }
    }
}
