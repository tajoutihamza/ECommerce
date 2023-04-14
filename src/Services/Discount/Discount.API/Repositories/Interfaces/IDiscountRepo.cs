using Discount.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.API.Repositories.Interfaces
{
    public interface IDiscountRepo
    {

        Task<Coupon> GetCoupon(string ProductName);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string ProductName);
    }
}
