using MvcMusicStores.Models;

namespace MvcMusicStores.ViewModels
{
    public class ShoppingCartViewMode
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
