using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly Context _context;
        public DiscountRepo(Context context) {
            _context = context;
        }
        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            _context.coupon.Add(coupon);
            _context.SaveChanges();
            return _context.SaveChanges() == 0;                
        }

        public async Task<bool> DeleteCoupon(string ProductName)
        {
            var coupon = await _context.coupon.FirstOrDefaultAsync<Coupon>(t => t.productName.Equals(ProductName));
            _context.coupon.Remove(coupon);
            _context.SaveChanges();
            return _context.SaveChanges() == 0;
        }

        public async Task<Coupon> GetCoupon(string ProductName)
        {
            return await _context.coupon.FirstOrDefaultAsync<Coupon>(t => t.productName.Equals(ProductName));
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            Coupon entity = await _context.Set<Coupon>().FindAsync(coupon.id);
            _context.Entry(entity).State = EntityState.Detached;
            _context.coupon.Update(coupon);
            _context.SaveChanges();
            return _context.SaveChanges() == 0;
        }
    }
}
