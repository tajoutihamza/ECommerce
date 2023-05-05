using System.ComponentModel.DataAnnotations;

namespace Discount.API.Entities
{
    public class Coupon
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string productname { get; set; }
        public string description { get; set; }
        [Required]
        public int amount { get; set; }
    }
}
