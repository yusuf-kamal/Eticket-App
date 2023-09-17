using Eticket.Models;
using ETicket.data;
using ETicket.Models;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Data.Cart
{
    public class ShoppingCart
    {
        private readonly EticketDbContext _context;

        public ShoppingCart( EticketDbContext context)
        {
           _context = context;
        }


        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        public static ShoppingCart GetShoppingCart( IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context=service.GetService<EticketDbContext>();
            string cartId=session.GetString("CartId") ??Guid.NewGuid().ToString();
            session.SetString("CartId", "cartId");
            return new ShoppingCart(context) { ShoppingCartId = cartId };


        }

        public void AddItemToCart( Movie movie)
        {
            var shoppingcartItem = _context.ShoppingCartItems
            .FirstOrDefault(m=>m.Movie.Id==movie.Id&&m.ShoppingCartId==ShoppingCartId);

            if(shoppingcartItem==null) 
            {
                shoppingcartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1

                };
                _context.ShoppingCartItems.Add(shoppingcartItem);
            }
            else
            {
                shoppingcartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart( Movie movie )
        {
            var shoppingcartItem = _context.ShoppingCartItems
            .FirstOrDefault(m => m.Movie.Id == movie.Id && m.ShoppingCartId == ShoppingCartId);

            if (shoppingcartItem != null)
            {
                if (shoppingcartItem.Amount > 1)
                {
                    shoppingcartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingcartItem);

                }
            }
            
            _context.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCartItem()
        {
            return ShoppingCartItems??(ShoppingCartItems=_context.ShoppingCartItems
                .Where(c=>c.ShoppingCartId==ShoppingCartId).Include(m=>m.Movie).ToList());
        }

        public double GetShoppingCartTottal()
        {
            var total=_context.ShoppingCartItems
                .Where(s=>s.ShoppingCartId==ShoppingCartId)
                .Select(n=>n.Movie.Price*n.Amount).Sum();
            return total;
        }



        public async Task ClearShoppingCartAsync()
        {
            var items=await _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            //ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}
