﻿using Discount.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Discount.API.Data
{
    public class DbPreparation
    {
        public static void DbInit(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<Context>());
            }
        }

        private static void SeedData(Context context)
        {
            Console.WriteLine("---> Lunching Migration:");
            try
            {
                context.Database.Migrate();
                if (!context.coupon.Any()) { 
                    context.AddRange(getPreconfiguredCoupons());
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static IEnumerable<Coupon> getPreconfiguredCoupons()
        {
            return new List<Coupon>()
            {
                new Coupon() {
                    id=1,
                    productname="IPhone X",
                    description="IPhone X discount",
                    amount=250
                },
                new Coupon() {
                    id=2,
                    productname="Samsung 10",
                    description="Samsung 10 discount",
                    amount=200
                },
                new Coupon() {
                    id=3,
                    productname="Huawei Plus",
                    description="Huawei Plus discount",
                    amount=350
                },
                new Coupon() {
                    id=4,
                    productname="LG G7 ThinQ",
                    description="LG G7 ThinQ discount",
                    amount=120
                },
            };
        }


    }
}
