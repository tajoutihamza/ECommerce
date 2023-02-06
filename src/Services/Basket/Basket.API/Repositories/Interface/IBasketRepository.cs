using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories.Interface
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
        Task<ShoppingCart> GetBasket(string username);
        Task deleteBasket(string username);
    }
}
