using Basket.API.Entities;
using Discount.Grpc.Protos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcServices
    {
        private readonly DiscountGrpc.DiscountGrpcClient _discountGrpcClient;
        public DiscountGrpcServices(DiscountGrpc.DiscountGrpcClient discountGrpcClient)
        {
            _discountGrpcClient = discountGrpcClient;
        }
        public async Task<ShoppingCart> GetDiscounts(ShoppingCart basket)
        {

            for (int i = 0; i < basket.Items.Count; i++)
            {
                var item = basket.Items[i];
                var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
                CouponModel coupon = await _discountGrpcClient.GetDiscountAsync(discountRequest);
                basket.Items[i].Price -= coupon.Amount;
            }
            return basket;
        }
    }
}
