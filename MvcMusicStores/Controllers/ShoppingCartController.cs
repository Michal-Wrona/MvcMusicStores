using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStores.Data;
using MvcMusicStores.Models;
using MvcMusicStores.ViewModels;
using System.Text.Encodings.Web;


namespace MvcMusicStores.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MvcMusicStoresContext _context;
        public ShoppingCartController(MvcMusicStoresContext context)
        {
            _context = context;
        }
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewMode
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View();
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedAlbum = _context.Album
                .Single(album => album.AlbumId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = _context.Carts
                .Single(item => item.RecordId == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            var encodedOutput = HtmlEncoder.Default.Encode(albumName);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {

            Message = encodedOutput +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);

        }
        //
        // GET: /ShoppingCart/CartSummary
         //[ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
