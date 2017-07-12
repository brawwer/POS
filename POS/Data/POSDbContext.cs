using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.Models;

namespace POS.Models
{
    public class POSDbContext : DbContext
    {
        public POSDbContext (DbContextOptions<POSDbContext> options)
            : base(options)
        {
        }

        public DbSet<POS.Models.InventoryItem> InventoryItem { get; set; }

        public DbSet<POS.Models.DailySalesModel> DailySalesModel { get; set; }

        public DbSet<POS.Models.DailySalesEntry> DailySalesEntry { get; set; }

        public DbSet<POS.Models.PaymentMethod> PaymentMethod { get; set; }

        public DbSet<POS.Models.AddInventoryModel> AddInventoryModel { get; set; }

    }
}
