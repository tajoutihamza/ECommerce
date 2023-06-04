using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService:DiscountGrpc.DiscountGrpcBase
    {
        private readonly IDiscountRepo _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepo repo, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<CouponModel> CreateDiscount(DiscountModel request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repo.CreateCoupon(coupon);
            _logger.LogInformation("Coupon is created successfully. ProductName : {ProductName}", coupon.productName);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repo.GetCoupon(request.ProductName);
            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,$"Discount with productName={request.ProductName} do not exist"));
            }
            _logger.LogInformation("Coupon is retreived successfully. ProductName : {ProductName}, Amount : {Amount}", coupon.productName,coupon.amount);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(DiscountModel request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repo.UpdateCoupon(coupon);
            _logger.LogInformation("Coupon is updated successfully. ProductName : {ProductName}, Amount : {Amount}", coupon.productName,coupon.amount);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repo.DeleteCoupon(request.ProductName);
            var response = new DeleteDiscountResponse{
                Success = deleted
            };
            if(deleted)
                _logger.LogInformation("Coupon is deleted successfully. ProductName : {ProductName}", request.ProductName);
            else
                throw new RpcException(new Status(StatusCode.Internal,$"Discount with productName={request.ProductName} is not deleted"));
            return response;
        }
    }
}
