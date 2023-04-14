namespace Discount.API.Entities
{
    public class Coupon
    {
        public int id { get; set; }
        public string productname { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
    }
}
