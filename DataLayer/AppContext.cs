using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class AppContext : DbContext
    {
        public AppContext (DbContextOptions option) : base(option)
        {

        }

        public DbSet<LaptopModel> LaptopModel { get; set; }

        public DbSet<OrderModel> OrderModel { get; set; }
    }
}
