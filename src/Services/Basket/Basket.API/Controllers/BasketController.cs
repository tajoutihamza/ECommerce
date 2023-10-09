using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcServices _discountGrpcServices;
        public BasketController(IBasketRepository repository, DiscountGrpcServices discountGrpcServices)
        {
            _repository = repository;
            _discountGrpcServices = discountGrpcServices;
        }
        [HttpGet("{username}",Name ="GetBasket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }
        [HttpDelete("{username}",Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _repository.deleteBasket(username);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            basket = await _discountGrpcServices.GetDiscounts(basket);
            return Ok( await _repository.UpdateBasket(basket));
        }
    }
}
