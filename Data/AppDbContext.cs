﻿using CLDV6211_POE_ST10267411_KhumaloCraft.Models;
using Microsoft.EntityFrameworkCore;

namespace CLDV6211_POE_ST10267411_KhumaloCraft.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }



    }
}
