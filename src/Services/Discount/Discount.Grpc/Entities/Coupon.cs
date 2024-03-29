﻿using System.ComponentModel.DataAnnotations;

namespace Discount.Grpc.Entities
{
    public class Coupon
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string productName { get; set; }
        public string description { get; set; }
        [Required]
        public int amount { get; set; }
    }
}
