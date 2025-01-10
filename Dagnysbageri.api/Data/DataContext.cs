using Dagnysbageri.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dagnysbageri.api.Data.Migrations
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SupplierProduct>().HasKey(s => new { s.ProductId, s.SupplierId });
        }
    }
}