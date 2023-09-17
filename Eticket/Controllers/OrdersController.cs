using Eticket.Data.Cart;
using Eticket.Data.MovieService;
using Eticket.Data.OrderServices;
using Eticket.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eticket.Controllers
{
	[Authorize]

	public class OrdersController : Controller
    {
        private readonly IMovie _movie;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrdersController( IMovie movie,ShoppingCart shoppingCart ,IOrderService orderService)
        {
           _movie = movie;
            _shoppingCart = shoppingCart;
           _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserRole = User.FindFirstValue(ClaimTypes.Role);
            var orders=await _orderService.GetOrderByUserIdAndRoleAsync(userId,UserRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItem();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTottal = _shoppingCart.GetShoppingCartTottal(),
            };
            return View(response); 
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var item =await _movie.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));

        }

        

        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movie.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));

        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItem();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string Email = User.FindFirstValue(ClaimTypes.Email);

            await _orderService.StoreOrderAsync(items, userId, Email);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        } 
    }
}
