﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items = new List<ShoppingCartItem>();

        public ShoppingCart(){}
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                for(int i=0;i<Items.Count;i++)
                {
                    totalPrice += Items[i].Price;
                }
                return totalPrice;
            }
        }
    }
}