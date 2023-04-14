using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController: ControllerBase
    {
        private readonly IDiscountRepo _repo;
        public DiscountController(IDiscountRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo)); 
        }

        [HttpGet("{ProductName}", Name = "GetDiscount")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> GetCoupon(string ProductName)
        {
            var coupon = await _repo.GetCoupon(ProductName);
            if (coupon != null) return Ok(coupon);
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        public async Task<ActionResult<Coupon>> CreateCoupon([FromBody] Coupon coupon)
        {
            await _repo.CreateCoupon(coupon);
            return CreatedAtAction(nameof(GetCoupon), new { ProductName = coupon.productname }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Coupon>> UpdateCoupon([FromBody] Coupon coupon)
        {
            if (await _repo.GetCoupon(coupon.productname) != null)
                return Ok(await _repo.UpdateCoupon(coupon));
            return NotFound();
        }

        [HttpDelete("{ProductName}", Name = "DeleteCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Coupon))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Coupon>> DeleteCoupon(string ProductName)
        {
            if (await _repo.GetCoupon(ProductName) != null)
                return Ok(await _repo.DeleteCoupon(ProductName));
            return NotFound();
        }

    }
}
